using HomeForPets.Domain.Shared.ValueObjects;

namespace HomeForPets.Application.FileProvider;

public record FileData(Stream Stream, FilePath FilePath, string BucketName);