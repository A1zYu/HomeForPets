using HomeForPets.Domain.Constraints;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Volunteers;
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
        .HasConversion(
            id => id.Value,
            value => PetId.Create(value)
        );

    builder.Property(p => p.Name)
        .HasColumnName("name")
        .IsRequired()
        .HasMaxLength(Constraints.LOW_VALUE_LENGTH);
    
    builder.ComplexProperty(p => p.Description, pb =>
    {
        pb.Property(x => x.Text)
            .HasColumnName("description")
            .IsRequired();
    });
    
    builder.ComplexProperty(p => p.PetDetails, pb =>
    {
        pb.Property(x => x.Color)
            .HasColumnName("color")
            .IsRequired();
        pb.Property(x => x.Height)
            .HasColumnName("height")
            .IsRequired();
        pb.Property(x => x.Weight)
            .HasColumnName("weight")
            .IsRequired();
        pb.Property(x => x.HealthInfo)
            .HasColumnName("health_info")
            .IsRequired();
        pb.Property(x => x.BirthOfDate)
            .HasColumnName("birth_of_date")
            .IsRequired();
        pb.Property(x => x.IsVaccinated)
            .HasColumnName("is_vaccinated")
            .IsRequired();
        pb.Property(x => x.IsNeutered)
            .HasColumnName("is_neutered")
            .IsRequired();
    });

    builder.ComplexProperty(p => p.SpeciesBreed, s =>
    {
        s.Property(x => x.BreedId)
            .HasColumnName("breed_id");
        s.Property(x => x.SpeciesId)
            .HasColumnName("species_id")
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value)
            );
    });
    
    builder.ComplexProperty(p => p.Address, a =>
    {
        a.Property(x => x.City)
            .HasColumnName("city")
            .IsRequired();
        a.Property(x => x.Street)
            .HasColumnName("street")
            .IsRequired();
        a.Property(x => x.HouseNumber)
            .HasColumnName("house_number")
            .IsRequired();
        a.Property(x => x.FlatNumber)
            .HasColumnName("flat_number")
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
        .OnDelete(DeleteBehavior.Cascade);
    
    builder.Navigation(p => p.PetPhotos).AutoInclude();

    builder.Property(p => p.CreatedDate)
        .IsRequired()
        .HasColumnName("create_date");
    
    builder.OwnsOne(p => p.PaymentDetailsList, pb =>
    {
        pb.ToJson("payment_details");
        pb.OwnsMany(p => p.PaymentDetails, sb =>
        {
            sb.Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired();
        
            sb.Property(p => p.Description)
                .HasColumnName("description")
                .IsRequired();
        });
    });
    builder.Property("_idDeleted")
        .UsePropertyAccessMode(PropertyAccessMode.Field)
        .HasColumnName("is_deleted");
}
}