﻿using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Species;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Infrastructure.Configurations.Write;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                Id => BreedId.Create(Id));
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Domain.Constraints.Constants.LOW_VALUE_LENGTH);

    }
}