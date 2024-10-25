using HomeForPets.Core.Dtos.SpeciesDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Species.Infrastructure.Configurations;

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