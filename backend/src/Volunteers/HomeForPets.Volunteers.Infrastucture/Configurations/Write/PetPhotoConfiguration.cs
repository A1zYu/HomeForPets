using HomeForPets.Core.Ids;
using HomeForPets.Volunteers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Volunteers.Infrastucture.Configurations.Write;

public class PetPhotoConfiguration : IEntityTypeConfiguration<PetPhoto>
{
    public void Configure(EntityTypeBuilder<PetPhoto> builder)
    {
        builder.ToTable("pet_photos");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                Id => PetPhotoId.Create(Id));
        builder.Property(p => p.Path).IsRequired().HasMaxLength(Core.Constants.HIGH_VALUE_LENGTH);
    }
}