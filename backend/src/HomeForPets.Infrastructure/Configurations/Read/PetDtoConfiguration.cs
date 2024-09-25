using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.Volunteers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");
        builder.HasKey(x => x.Id);
        
        builder.HasMany(p=>p.PetPhotos)
            .WithOne()
            .HasForeignKey(p=>p.PetId);
    }
}