using CSharpFunctionalExtensions;
using HomeForPets.Application.File.Create;
using HomeForPets.Application.FileProvider;
using HomeForPets.Application.Providers;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.ValueObjects;
using Microsoft.Extensions.Logging;

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
        
        var fileContent = new FileData(null!, filePath.Value, BUCKET_NAME);
        
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