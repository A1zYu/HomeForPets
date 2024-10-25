using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Species;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Infrastructure.Configurations.Write;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                Id => SpeciesId.Create(Id));
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Domain.Constraints.Constants.LOW_VALUE_LENGTH);
        builder.HasMany(x => x.Breeds)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(x => x.Breeds).AutoInclude();
    }
}