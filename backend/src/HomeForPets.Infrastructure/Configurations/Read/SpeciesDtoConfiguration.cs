using HomeForPets.Application.Database;
using HomeForPets.Application.Dtos.SpeciesDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Infrastructure.Configurations.Read;

public class SpeciesDtoConfiguration : IEntityTypeConfiguration<SpeciesDto>
{
    public void Configure(EntityTypeBuilder<SpeciesDto> builder)
    {
        builder.ToTable("species");
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.BreedDtos)
            .WithOne()
            .HasForeignKey(x => x.SpeciesId);
    }
}