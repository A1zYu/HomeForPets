using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;

namespace HomeForPets.Application.Files;

public interface IFileProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> fileData, CancellationToken cancellationToken = default);

    Task<Result<string, Error>> DeleteFile(FileInfo fileData,CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetFile(FileData fileData, CancellationToken cancellationToken = default);
    Task<UnitResult<Error>> RemoveFile(FileInfo fileData, CancellationToken cancellationToken = default);
}