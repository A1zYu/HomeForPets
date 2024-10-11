using CSharpFunctionalExtensions;
using HomeForPets.Core;
using FileInfo = HomeForPets.Core.FileInfo;

namespace HomeForPets.Volunteers.Application;

public interface IFileProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> fileData, CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> DeleteFile(FileInfo fileData,CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetFile(FileData fileData, CancellationToken cancellationToken = default);
    Task<UnitResult<Error>> RemoveFile(FileInfo fileData, CancellationToken cancellationToken = default);
}