using System.Text.Json;
using HomeForPets.Domain.Constraints;
using HomeForPets.Domain.Models.Volunteer;
using HomeForPets.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

        builder.OwnsOne(v => v.Contact, contact =>
        {
            contact.Property(x => x.PhoneNumber)
                .HasConversion(
                    phoneNumber => phoneNumber.Number,
                    value => PhoneNumber.Create(value).Value);

            var socialNetworksConverter = new ValueConverter<List<SocialNetwork>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<SocialNetwork>>(v, (JsonSerializerOptions)null),
                new ConverterMappingHints(size: 5000)
            );

            contact.Property(c => c.SocialNetworks)
                .HasConversion(socialNetworksConverter)
                .HasColumnType("jsonb");
        });
        
        builder.ComplexProperty(x => x.FullName,
            f =>
            {
                f.Property(p => p.FirstName)
                    .HasMaxLength(Constraints.LOW_VALUE_LENGTH)
                    .IsRequired();
                f.Property(p => p.LastName)
                    .HasMaxLength(Constraints.LOW_VALUE_LENGTH)
                    .IsRequired();
                f.Property(p => p.MiddleName)
                    .HasMaxLength(Constraints.LOW_VALUE_LENGTH)
                    .IsRequired();
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