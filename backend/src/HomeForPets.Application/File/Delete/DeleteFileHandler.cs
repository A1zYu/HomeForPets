using CSharpFunctionalExtensions;
using HomeForPets.Application.File.Get;
using HomeForPets.Application.FileProvider;
using HomeForPets.Application.Providers;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.ValueObjects;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.File.Delete;

public class DeleteFileHandler
{
    private const string BUCKET_NAME = "pet-photos";
    private readonly ILogger<DeleteFileHandler> _logger;
    private readonly IFileProvider _provider;

    public DeleteFileHandler(ILogger<DeleteFileHandler> logger, IFileProvider provider)
    {
        _logger = logger;
        _provider = provider;
    }
    public async Task<Result<string, Error>> Handle(
        DeleteFileRequest request,
        CancellationToken cancellationToken
    )
    {
        var filePath = FilePath.Create(request.FullPath);
        
        var fileContent = new FileData(null!, filePath.Value, BUCKET_NAME);
        
        var result = await _provider.DeleteFile(fileContent, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error;
        }
        
        _logger.LogInformation("File:{path} deleted",filePath.Value);
        
        return result.Value;
    }
}
public record DeleteFileRequest(string FullPath);