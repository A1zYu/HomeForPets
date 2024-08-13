using HomeForPets.Domain.Constraints;
using HomeForPets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(v => v.Description).IsRequired().HasMaxLength(Constraints.HIGH_VALUE_LENGTH);
        builder.Property(v => v.PhoneNumber).IsRequired().HasMaxLength(Constraints.LOW_VALUE_LENGTH);
        builder.Property(v => v.FullName).IsRequired().HasMaxLength(Constraints.LOW_VALUE_LENGTH);

        builder
            .HasMany(x => x.PaymentDetailsList)
            .WithOne()
            .HasForeignKey(x=>x.VolunteerId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(x => x.Pets).WithOne()
            .HasForeignKey(x => x.VolunteerId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(x => x.SocialNetworks)
            .WithOne().HasForeignKey(x => x.VolunteerId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}