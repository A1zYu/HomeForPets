using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.SharedKernel;

namespace HomeForPets.Volunteers.Application;

public interface IFileProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> fileData, CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> DeleteFile(FileInfoCommnad fileData,CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetFile(FileData fileData, CancellationToken cancellationToken = default);
    Task<UnitResult<Error>> RemoveFile(FileInfoCommnad fileData, CancellationToken cancellationToken = default);
}