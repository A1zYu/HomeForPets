using HomeForPets.SharedKernel.Ids;
using HomeForPets.Species.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Species.Infrastructure.Configurations.Write;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Core.Constants.LOW_VALUE_LENGTH);

    }
}