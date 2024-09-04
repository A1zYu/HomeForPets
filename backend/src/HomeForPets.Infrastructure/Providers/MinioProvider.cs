using System.Reactive.Linq;
using CSharpFunctionalExtensions;
using HomeForPets.Application.FileProvider;
using HomeForPets.Application.Providers;
using HomeForPets.Domain.Shared;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;

namespace HomeForPets.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;
    private const int PresignedUrlExpiryInSeconds = 60 * 60 * 24;
    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }
    public async Task<Result<string,Error>> UploadFile(
        FileData fileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket("pet-photos");
            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
            if (bucketExist==false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket("pet-photos");
                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        
            var putObject = new PutObjectArgs()
                .WithBucket("pet-photos")
                .WithStreamData(fileData.Stream)
                .WithObjectSize(fileData.Stream.Length)
                .WithObject(fileData.FilePath.Path);
        
            var result = await _minioClient.PutObjectAsync(putObject, cancellationToken);
            return result.ObjectName;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Error to upload file in minio");
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
    }
    
    public async Task<Result<string, Error>> DeleteFile(FileData fileData, CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket(fileData.BucketName);
            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
            if (bucketExist == false)
            {
                _logger.LogError("Not found bucket : '{id}' file",fileData.BucketName);
                throw new ApplicationException("Not found bucket file");
            }

            var removeObject = new RemoveObjectArgs()
                .WithBucket("pet-photos")
                .WithObject(fileData.FilePath.Path);

            await _minioClient.RemoveObjectAsync(removeObject, cancellationToken);
            
            return fileData.FilePath.Path;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Error to deleted file in minio");
            return Error.Failure("file.upload", "Fail to delete file in minio");
        }
    }

    public async Task<Result<string,Error>> GetFile(FileData fileData, CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket(fileData.BucketName);
            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
            if (bucketExist == false)
            {
                _logger.LogError("Not found bucket : '{id}' file",fileData.BucketName);
                throw new ApplicationException("Not found bucket file");
            }

            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.FilePath.Path)
                .WithExpiry(PresignedUrlExpiryInSeconds);;
            var result = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);;
            if (string.IsNullOrEmpty(result))
            {
                _logger.LogError("Empty url for file");
                return Error.NotFound("file.get", "Fail not found in minio");
            }
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Error to get file in minio");
            return Error.NotFound("file.get", "Fail not found in minio");
        }  
    }

    public async Task<Result<List<string>, Error>> GetFiles(FileData fileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket(fileData.BucketName);
            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
            if (bucketExist == false)
            {
                _logger.LogError("Not found bucket : '{id}' file",fileData.BucketName);
                throw new ApplicationException("Not found bucket file");
            }
            var fileNames = new List<string>();
        
            var listObjectsArgs = new ListObjectsArgs()
                .WithBucket(fileData.BucketName)
                .WithRecursive(true);
            var listObjects = _minioClient.ListObjectsAsync(listObjectsArgs, cancellationToken);
            using var itemsFile = listObjects.Subscribe(x => fileNames.Add(x.Key),ex=>_logger.LogError(ex,ex.Message));
            return fileNames;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}