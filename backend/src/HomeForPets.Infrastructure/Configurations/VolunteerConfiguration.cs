using System.Text.Json;
using HomeForPets.Domain.Constraints;
using HomeForPets.Domain.Models.Volunteer;
using HomeForPets.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.VisualBasic;

namespace HomeForPets.Infrastructure.Configurations;

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
            .HasMaxLength(Constraints.HIGH_VALUE_LENGTH);
        
        builder.OwnsOne(v => v.Contact, cb =>
        {
            cb.ToJson();
            cb.Property(x => x.PhoneNumber)
                .HasConversion(
                    phoneNumber => phoneNumber.Number,
                    value => PhoneNumber.Create(value).Value);
            cb.OwnsMany(d => d.SocialNetworks, sb =>
            {
                sb.Property(f => f.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(Constraints.LOW_VALUE_LENGTH);
                sb.Property(f => f.Path)
                    .IsRequired()
                    .HasColumnName("path");
            });
        });
        
        builder.ComplexProperty(x => x.FullName,
            f =>
            {
                f.Property(p => p.FirstName)
                    .HasMaxLength(Constraints.LOW_VALUE_LENGTH)
                    .HasColumnName("first_name")
                    .IsRequired();
                f.Property(p => p.LastName)
                    .HasMaxLength(Constraints.LOW_VALUE_LENGTH)
                    .HasColumnName("last_name")
                    .IsRequired();
                f.Property(p => p.MiddleName)
                    .HasMaxLength(Constraints.LOW_VALUE_LENGTH)
                    .HasColumnName("middle_name")
                    .IsRequired(false);
            });
        
        builder.OwnsMany(x => x.PaymentDetailsList, p =>
        {
            p.ToJson();
            p.Property(y => y.Name)
                .IsRequired()
                .HasColumnName("payment_details_name");
            p.Property(y => y.Description)
                .IsRequired()
                .HasColumnName("payment_details_description");
        });
        builder.HasMany(x => x.Pets)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

    }
}