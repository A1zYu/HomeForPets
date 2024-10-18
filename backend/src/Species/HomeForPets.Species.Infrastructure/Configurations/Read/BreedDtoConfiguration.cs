using HomeForPets.Core.Dtos.SpeciesDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Species.Infrastructure.Configurations;

public class BreedDtoConfiguration : IEntityTypeConfiguration<BreedDto>
{
    public void Configure(EntityTypeBuilder<BreedDto> builder)
    {
        builder.ToTable("breeds");
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.SpeciesId)
            .IsRequired();
        builder.Property(x=>x.Name).HasMaxLength(Core.Constants.LOW_VALUE_LENGTH);
    }
}