using HomeForPets.Application.Dtos.Volunteers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Infrastructure.Configurations.Read;

public class PetPhotoConfiguration : IEntityTypeConfiguration<PetsPhotoDto>
{
    public void Configure(EntityTypeBuilder<PetsPhotoDto> builder)
    {
        builder.ToTable("pet_photos");
    }
}