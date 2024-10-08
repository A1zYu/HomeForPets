﻿using HomeForPets.Domain.VolunteersManagement.ValueObjects;

namespace HomeForPets.Application.Files;

public record FileData(Stream Stream,FileInfo Info);
public record FileInfo( FilePath FilePath, string BucketName);