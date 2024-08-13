using HomeForPets.Domain.Constraints;
using HomeForPets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Configurations;

public class PetPhotoConfiguration : IEntityTypeConfiguration<PetPhoto>
{
    public void Configure(EntityTypeBuilder<PetPhoto> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Path).IsRequired().HasMaxLength(Constraints.HIGH_VALUE_LENGTH);
    }
}