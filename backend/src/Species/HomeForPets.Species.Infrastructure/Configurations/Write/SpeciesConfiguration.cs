using HomeForPets.SharedKernel.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Species.Infrastructure.Configurations.Write;

public class SpeciesConfiguration : IEntityTypeConfiguration<Domain.Species>
{
    public void Configure(EntityTypeBuilder<Domain.Species> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value));
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Core.Constants.LOW_VALUE_LENGTH);
        builder.HasMany(x => x.Breeds)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.Navigation(x => x.Breeds).AutoInclude();
    }
}