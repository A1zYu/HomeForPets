using HomeForPets.Core.Dtos.Volunteers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Volunteers.Infrastucture.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");
        builder.HasKey(x => x.Id);
        
        builder.HasMany(p=>p.PetPhotos)
            .WithOne()
            .HasForeignKey(p=>p.PetId);
        builder.Navigation(x => x.PetPhotos).AutoInclude();
    }
}