using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.SharedKernel;
using HomeForPets.Volunteers.Application;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;

namespace HomeForPets.Volunteers.Infrastucture.Providers;

public class MinioProvider : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;
    private const int PresignedUrlExpiryInSeconds = 60 * 60 * 24;
    private const int MAX_DEGREE_OF_PARALLELISM = 10;

    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);
        var filesList = filesData.ToList();

        try
        {
            await IfBucketsNotExistCreateBucket(filesList.Select(x => x.InfoCommnad.BucketName), cancellationToken);

            var tasks = filesList.Select(async file =>
                await PutObject(file, semaphoreSlim, cancellationToken));

            var pathsResult = await Task.WhenAll(tasks);

            if (pathsResult.Any(p => p.IsFailure))
                return pathsResult.First().Error;

            var results = pathsResult.Select(p => p.Value).ToList();

            _logger.LogInformation("Uploaded files: {files}", results.Select(f => f.Path));

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload files in minio, files amount: {amount}", filesList.Count);

            return Error.Failure("file.upload", "Fail to upload files in minio");
        }
    }

    public async Task<UnitResult<Error>> DeleteFile(FileInfoCommnad fileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await IfBucketsNotExistCreateBucket([fileData.BucketName], cancellationToken);

            var removeObject = new RemoveObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.FilePath.Path);

            await _minioClient.RemoveObjectAsync(removeObject, cancellationToken);

            return UnitResult.Success<Error>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error to deleted file in minio");
            return Error.Failure("file.upload", "Fail to delete file in minio");
        }
    }

    public async Task<Result<string, Error>> GetFile(FileData fileData, CancellationToken cancellationToken = default)
    {
        try
        {
            await IfBucketsNotExistCreateBucket([fileData.InfoCommnad.BucketName], cancellationToken);

            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(fileData.InfoCommnad.BucketName)
                .WithObject(fileData.InfoCommnad.FilePath.Path)
                .WithExpiry(PresignedUrlExpiryInSeconds);
            ;
            var result = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);
            ;
            if (string.IsNullOrEmpty(result))
            {
                _logger.LogError("Empty url for file");
                return Error.NotFound("file.get", "Fail not found in minio");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to get file in minio");
            return Error.NotFound("file.get", "Fail not found in minio");
        }
    }

    public async Task<UnitResult<Error>> RemoveFile(FileInfoCommnad fileInfoCommnad, CancellationToken cancellationToken = default)
    {
        try
        {
            await IfBucketsNotExistCreateBucket([fileInfoCommnad.BucketName], cancellationToken);

            var statArgs = new StatObjectArgs()
                .WithBucket(fileInfoCommnad.BucketName)
                .WithObject(fileInfoCommnad.FilePath.Path);

            var objectResult = await _minioClient.StatObjectAsync(statArgs, cancellationToken);
            if (objectResult is null)
            {
                return Result.Success<Error>();
            }

            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(fileInfoCommnad.BucketName)
                .WithObject(fileInfoCommnad.FilePath.Path);

            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to remove file in minio with path {path} in backet {backet}",
                fileInfoCommnad.FilePath.Path,
                fileInfoCommnad.BucketName);
            return Error.Failure("file.remove", "Fail to remove file in minio");
        }

        return Result.Success<Error>();
    }

    public async Task<Result<List<string>, Error>> GetFiles(FileData fileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await IfBucketsNotExistCreateBucket([fileData.InfoCommnad.BucketName], cancellationToken);

            var fileNames = new List<string>();

            var listObjectsArgs = new ListObjectsArgs()
                .WithBucket(fileData.InfoCommnad.BucketName)
                .WithRecursive(true);

            var listObjects = _minioClient.ListObjectsAsync(listObjectsArgs, cancellationToken);

            using var itemsFile =
                listObjects.Subscribe(x => fileNames.Add(x.Key), ex => _logger.LogError(ex, ex.Message));

            return fileNames;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    private async Task<Result<FilePath, Error>> PutObject(
        FileData fileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileData.InfoCommnad.BucketName)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(fileData.InfoCommnad.FilePath.Path);

        try
        {
            await _minioClient
                .PutObjectAsync(putObjectArgs, cancellationToken);

            return fileData.InfoCommnad.FilePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload file in minio with path {path} in bucket {bucket}",
                fileData.InfoCommnad.FilePath.Path,
                fileData.InfoCommnad.BucketName);

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task IfBucketsNotExistCreateBucket(
        IEnumerable<string> backets,
        CancellationToken cancellationToken)
    {
        HashSet<string> bucketNames = [..backets];

        foreach (var bucketName in bucketNames)
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

            var bucketExist = await _minioClient
                .BucketExistsAsync(bucketExistArgs, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }
}