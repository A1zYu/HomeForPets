using HomeForPets.Core.Dtos.Volunteers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Volunteers.Infrastucture.Configurations.Read;

public class PetPhotoConfiguration : IEntityTypeConfiguration<PetsPhotoDto>
{
    public void Configure(EntityTypeBuilder<PetsPhotoDto> builder)
    {
        builder.ToTable("pet_photos");
    }
}