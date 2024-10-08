﻿using CSharpFunctionalExtensions;
using HomeForPets.Application.File.Create;
using HomeForPets.Application.Files;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;
using Microsoft.Extensions.Logging;
using FileInfo = HomeForPets.Application.Files.FileInfo;

namespace HomeForPets.Application.File.Get;

public class GetFileHandler
{
    private const string BUCKET_NAME = "pet-photos";
    private readonly ILogger<GetFileHandler> _logger;
    private readonly IFileProvider _provider;

    public GetFileHandler(ILogger<GetFileHandler> logger, IFileProvider provider)
    {
        _logger = logger;
        _provider = provider;
    }

    public async Task<Result<string, Error>> Handle(
        GetFileRequest request,
        CancellationToken cancellationToken
    )
    {
        var filePath = FilePath.Create(request.FullPath);
        
        var fileContent = new FileData(null!, new FileInfo(filePath.Value, BUCKET_NAME));
        
        var result = await _provider.GetFile(fileContent, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error;
        }
        
        _logger.LogInformation("The file:{path} received",filePath.Value);
        
        return result.Value;
    }
}
public record GetFileRequest(string FullPath);