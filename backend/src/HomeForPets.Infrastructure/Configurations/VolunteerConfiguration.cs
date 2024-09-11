using HomeForPets.Domain.Constraints;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.VolunteersManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                    .HasMaxLength(Constants.LOW_VALUE_LENGTH)
                    .HasColumnName("first_name")
                    .IsRequired();
                f.Property(p => p.LastName)
                    .HasMaxLength(Constants.LOW_VALUE_LENGTH)
                    .HasColumnName("last_name")
                    .IsRequired();
                f.Property(p => p.MiddleName)
                    .HasMaxLength(Constants.LOW_VALUE_LENGTH)
                    .HasColumnName("middle_name")
                    .IsRequired(false);
            });
        builder.OwnsOne(v => v.SocialNetworkList, sb =>
        {
            sb.ToJson("social_network");
            sb.OwnsMany(s => s.SocialNetworks, snB =>
            {
                snB.Property(p => p.Name)
                    .HasColumnName("name")
                    .IsRequired();
            
                snB.Property(p => p.Path)
                    .HasColumnName("path")
                    .IsRequired();
            });
        });
        builder.OwnsOne(v => v.PaymentDetailsList, pb =>
        {
            pb.ToJson("payment_details");
            pb.OwnsMany(p => p.PaymentDetails,sb =>
            {
                sb.Property(p => p.Name)
                    .HasColumnName("name")
                    .IsRequired();
            
                sb.Property(p => p.Description)
                    .HasColumnName("description")
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