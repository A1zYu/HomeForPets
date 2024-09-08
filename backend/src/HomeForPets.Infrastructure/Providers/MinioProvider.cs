﻿using System.Reactive.Linq;
using CSharpFunctionalExtensions;
using HomeForPets.Application.FileProvider;
using HomeForPets.Application.Providers;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.ValueObjects;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;

namespace HomeForPets.Infrastructure.Providers;

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
            await IfBucketsNotExistCreateBucket(filesList, cancellationToken);

            var tasks = filesList.Select(async file =>
                await PutObject(file, semaphoreSlim, cancellationToken));

            var pathsResult = await Task.WhenAll(tasks);

            if (pathsResult.Any(p => p.IsFailure))
                return pathsResult.First().Error;

            var results = pathsResult.Select(p => p.Value).ToList();

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload files in minio, files amount: {amount}", filesList.Count);

            return Error.Failure("file.upload", "Fail to upload files in minio");
        }
    }
    
    public async Task<Result<string, Error>> DeleteFile(FileData fileData, CancellationToken cancellationToken = default)
    {
        try
        {
            await IfBucketsNotExistCreateBucket([fileData], cancellationToken);

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
            await IfBucketsNotExistCreateBucket([fileData], cancellationToken);

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
            await IfBucketsNotExistCreateBucket([fileData], cancellationToken);
            
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
    private async Task<Result<FilePath, Error>> PutObject(
        FileData fileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileData.BucketName)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(fileData.FilePath.Path);

        try
        {
            await _minioClient
                .PutObjectAsync(putObjectArgs, cancellationToken);

            return fileData.FilePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload file in minio with path {path} in bucket {bucket}",
                fileData.FilePath.Path,
                fileData.BucketName);

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }
    private async Task IfBucketsNotExistCreateBucket(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken)
    {
        HashSet<string> bucketNames = [..filesData.Select(file => file.BucketName)];

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