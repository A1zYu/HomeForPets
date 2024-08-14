using HomeForPets.Models;
using HomeForPets.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value)
                );
        builder.Property(v => v.Description)
            .IsRequired()
            .HasMaxLength(Constraints.Constraints.HIGH_VALUE_LENGTH);
        builder.Property(v => v.PhoneNumber)
            .IsRequired()
            .HasMaxLength(Constraints.Constraints.LOW_VALUE_LENGTH);

        builder.ComplexProperty(x => x.FullName,
            f =>
            {
                f.Property(p => p.FirstName)
                    .HasMaxLength(Constraints.Constraints.LOW_VALUE_LENGTH)
                    .IsRequired();
                f.Property(p => p.LastName)
                    .HasMaxLength(Constraints.Constraints.LOW_VALUE_LENGTH)
                    .IsRequired();
                f.Property(p => p.MiddleName)
                    .HasMaxLength(Constraints.Constraints.LOW_VALUE_LENGTH)
                    .IsRequired();
            });
        
        builder.HasMany(x => x.PaymentDetailsList)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Pets)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.SocialNetworks)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

    }
}