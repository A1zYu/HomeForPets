using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;

namespace HomeForPets.Application.FileProvider;

public record FileData(Stream Stream, FilePath FilePath, string BucketName);