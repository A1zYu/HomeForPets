using HomeForPets.Core.Ids;
using HomeForPets.Species.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Species.Infrastructure;

public class SpeciesConfiguration : IEntityTypeConfiguration<Specie>
{
    public void Configure(EntityTypeBuilder<Specie> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                Id => SpeciesId.Create(Id));
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Core.Constants.LOW_VALUE_LENGTH);
        builder.HasMany(x => x.Breeds)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(x => x.Breeds).AutoInclude();
    }
}