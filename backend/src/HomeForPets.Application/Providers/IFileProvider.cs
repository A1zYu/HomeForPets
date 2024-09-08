using CSharpFunctionalExtensions;
using HomeForPets.Application.FileProvider;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.ValueObjects;

namespace HomeForPets.Application.Providers;

public interface IFileProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> fileData, CancellationToken cancellationToken = default);

    Task<Result<string, Error>> DeleteFile(FileData fileData,CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetFile(FileData fileData, CancellationToken cancellationToken = default);
}