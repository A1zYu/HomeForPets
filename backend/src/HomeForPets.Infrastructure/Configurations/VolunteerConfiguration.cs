using System.Text.Json;
using HomeForPets.Domain.Constraints;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;
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
        builder.ComplexProperty(v => v.Description, vb =>
        {
            vb.Property(x => x.Text)
                .HasColumnName("description")
                .IsRequired();
        });
        builder.ComplexProperty(v => v.PhoneNumber, vb =>
        {
            vb.Property(x => x.Number)
                .HasColumnName("phone_number")
                .IsRequired();
        });

        builder.OwnsMany(x => x.SocialNetwork, sb =>
        {
            sb.ToJson();
            sb.Property(p => p.Name)
                .HasColumnName("social_network_name")
                .IsRequired();
            
            sb.Property(p => p.Path)
                .HasColumnName("social_network_path")
                .IsRequired();
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