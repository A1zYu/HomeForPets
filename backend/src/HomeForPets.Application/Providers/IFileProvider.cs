using CSharpFunctionalExtensions;
using HomeForPets.Application.FileProvider;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Application.Providers;

public interface IFileProvider
{
    Task<Result<string, Error>> UploadFile(
        FileData fileData, CancellationToken cancellationToken = default);

    Task<Result<string, Error>> DeleteFile(FileData fileData,CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetFile(FileData fileData, CancellationToken cancellationToken = default);
}