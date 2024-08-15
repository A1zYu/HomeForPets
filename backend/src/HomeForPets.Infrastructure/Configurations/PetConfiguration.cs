using HomeForPets.Domain.Constraints;
using HomeForPets.Domain.Models.Pet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value)
            );
        builder.Property(p => p.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(Constraints.LOW_VALUE_LENGTH);
        builder.Property(p => p.Description)
            .IsRequired()
            .HasColumnName("description")
            .HasMaxLength(Constraints.HIGH_VALUE_LENGTH);
        
        builder.Property(p => p.Breed)
            .HasColumnName("breed")
            .IsRequired()
            .HasMaxLength(Constraints.HIGH_VALUE_LENGTH);
        builder.Property(p => p.Species)
            .HasColumnName("species")
            .IsRequired()
            .HasMaxLength(Constraints.HIGH_VALUE_LENGTH);
        
        
        builder.Property(p => p.Color)
            .HasColumnName("color")
            .IsRequired()
            .HasMaxLength(Constraints.LOW_VALUE_LENGTH);
        builder.Property(p => p.Height)
            .HasColumnName("height")
            .IsRequired();
        builder.Property(p => p.Weight)
            .HasColumnName("weight")
            .IsRequired();
       
        builder.Property(p => p.HealthInfo)
            .IsRequired()
            .HasMaxLength(Constraints.LOW_VALUE_LENGTH);

        builder.ComplexProperty(p => p.Address, a =>
        {
            a.Property(x => x.City)
                .HasColumnName("city")
                .IsRequired();
            a.Property(x => x.District)
                .HasColumnName("district")
                .IsRequired();
            a.Property(x => x.FlatNumber)
                .HasColumnName("flat_number")
                .IsRequired();
            a.Property(x => x.HouseNumber)
                .HasColumnName("house_number")
                .IsRequired();
        });
        builder.ComplexProperty(p => p.PhoneNumberOwner, a =>
        {
            a.Property(x => x.Number)
                .IsRequired()
                .HasMaxLength(Constraints.LOW_VALUE_LENGTH)
                .HasColumnName("phone_number");
        });
        builder.HasMany(x => x.PetPhotos)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("pet_photos");
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
    }
}