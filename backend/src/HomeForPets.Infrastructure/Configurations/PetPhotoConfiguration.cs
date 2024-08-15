using HomeForPets.Domain.Constraints;
using HomeForPets.Domain.Models.Pet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Infrastructure.Configurations;

public class PetPhotoConfiguration : IEntityTypeConfiguration<PetPhoto>
{
    public void Configure(EntityTypeBuilder<PetPhoto> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                Id => PetPhotoId.Create(Id));
        builder.Property(p => p.Path).IsRequired().HasMaxLength(Constraints.HIGH_VALUE_LENGTH);
    }
}