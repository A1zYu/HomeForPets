using CSharpFunctionalExtensions;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.FilesDto;
using HomeForPets.Application.Files;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;
using Microsoft.Extensions.Logging;
using FileInfo = HomeForPets.Application.Files.FileInfo;

namespace HomeForPets.Application.File.Create;

public class CreateFileHandler
{
    private const string BUCKET_NAME = "pet-photos";
    private readonly ILogger<CreateFileHandler> _logger;
    private readonly IFileProvider _provider;

    public CreateFileHandler(ILogger<CreateFileHandler> logger, IFileProvider provider)
    {
        _logger = logger;
        _provider = provider;
    }

    public async Task<Result<string, Error>> Handle(
        CreateFileDto file,
        CancellationToken cancellationToken=default)
    {
        var extension = Path.GetExtension(file.FileName);

        var filePath = FilePath.Create(Guid.NewGuid(), extension);
        if (filePath.IsFailure)
            return filePath.Error;
        var fileContent = new FileData(file.Content,new FileInfo(filePath.Value, BUCKET_NAME));
        // var uploadFile =await _provider.UploadFile(fileContent, cancellationToken);
        // if (uploadFile.IsFailure)
        // {
        //     return uploadFile.Error;
        // }
        // _logger.LogInformation("File {file} saved",filePath.Value.Path);
        return filePath.Value.Path;
    }
}
