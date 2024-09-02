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
        builder.OwnsOne(v => v.SocialNetworkList, sb =>
        {
            sb.ToJson("social_network");
            sb.OwnsMany(s => s.SocialNetworks, snB =>
            {
                snB.Property(p => p.Name)
                    .HasColumnName("social_network_name")
                    .IsRequired();
            
                snB.Property(p => p.Path)
                    .HasColumnName("social_network_path")
                    .IsRequired();
            });
        });
        builder.OwnsOne(v => v.PaymentDetailsList, pb =>
        {
            pb.ToJson("payment_details");
            pb.OwnsMany(p => p.PaymentDetails,sb =>
            {
                sb.Property(p => p.Name)
                    .HasColumnName("payment_details_name")
                    .IsRequired();
            
                sb.Property(p => p.Description)
                    .HasColumnName("payment_details_description")
                    .IsRequired();
            });
        });
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