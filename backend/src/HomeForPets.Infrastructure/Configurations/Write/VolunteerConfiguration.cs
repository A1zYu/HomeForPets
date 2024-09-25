using System.Text.Json;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.Volunteers;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.VolunteersManagement;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Infrastructure.Configurations.Write;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

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
        builder.ComplexProperty(v => v.YearsOfExperience, yb =>
        {
            yb.Property(x => x.Value)
                .IsRequired(false)
                .HasColumnName("years_of_experience")
                .IsRequired();
        });
        builder.ComplexProperty(x => x.FullName,
            f =>
            {
                f.Property(p => p.FirstName)
                    .HasMaxLength(Domain.Constraints.Constants.LOW_VALUE_LENGTH)
                    .HasColumnName("first_name")
                    .IsRequired();
                f.Property(p => p.LastName)
                    .HasMaxLength(Domain.Constraints.Constants.LOW_VALUE_LENGTH)
                    .HasColumnName("last_name")
                    .IsRequired();
                f.Property(p => p.MiddleName)
                    .HasMaxLength(Domain.Constraints.Constants.LOW_VALUE_LENGTH)
                    .HasColumnName("middle_name")
                    .IsRequired(false);
            });
        
        builder.Property(v => v.PaymentDetails)
            .HasConversion(
                details => JsonSerializer.Serialize(details, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<PaymentDetailsDto>>(json,
                        JsonSerializerOptions.Default)!
                    .Select(dto => PaymentDetails.Create(dto.Name, dto.Description).Value)
                    .ToList(),
                new ValueComparer<IReadOnlyList<PaymentDetails>>(
                    (c1,c2)=> c1!.SequenceEqual(c2!),
                    c=> c.Aggregate(0,(a,v)=>HashCode.Combine(a,v.GetHashCode())),
                    c => (IReadOnlyList<PaymentDetails>)c.ToList())
            ).HasColumnName("payment_details");
        
        builder.Property(v => v.SocialNetworks)
            .HasConversion(
                socialNetwork => JsonSerializer.Serialize(socialNetwork, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<SocialNetworkDto>>(json,
                        JsonSerializerOptions.Default)!
                    .Select(dto => SocialNetwork.Create(dto.Name, dto.Path).Value)
                    .ToList(),
                new ValueComparer<IReadOnlyList<SocialNetwork>>(
                    (c1,c2)=> c1!.SequenceEqual(c2!),
                    c=> c.Aggregate(0,(a,v)=>HashCode.Combine(a,v.GetHashCode())),
                    c => (IReadOnlyList<SocialNetwork>)c.ToList())
            ).HasColumnName("social_networks");
       
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(v => v.Pets).AutoInclude();

        builder.Property("_idDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}