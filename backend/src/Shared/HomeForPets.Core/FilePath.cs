using CSharpFunctionalExtensions;
using HomeForPets.SharedKernel;

namespace HomeForPets.Core;

public record FilePath
{
    private FilePath(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public static Result<FilePath, Error> Create(Guid path, string extension)
    {
        if (string.IsNullOrWhiteSpace(extension) || extension.Length > Constants.LOW_VALUE_LENGTH)
        {
            return Errors.General.ValueIsInvalid("extensions");
        }

        if (!Constants.SUPPORTED_IMAGES_EXTENSIONS.Contains(extension.Trim()))
        {
            return Errors.General.ValueIsInvalid("extensions");
        }

        var fullPath = path + extension;

        return new FilePath(fullPath);
    }
    
    public static Result<FilePath, Error> Create(string fullPath)
    {
        return new FilePath(fullPath);
    }
}