﻿using HomeForPets.Domain.Constraints;
using HomeForPets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace HomeForPets.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(Constraints.LOW_VALUE_LENGTH);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(Constraints.HIGH_VALUE_LENGTH);
        builder.Property(p => p.Species).IsRequired().HasMaxLength(Constraints.HIGH_VALUE_LENGTH);
        builder.Property(p => p.Breed).IsRequired().HasMaxLength(Constraints.HIGH_VALUE_LENGTH);
        builder.Property(p => p.Color).IsRequired().HasMaxLength(Constraints.LOW_VALUE_LENGTH);
        builder.Property(p => p.Height).IsRequired();
        builder.Property(p => p.Weight).IsRequired();
        builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(Constraints.LOW_VALUE_LENGTH);
        builder.Property(p => p.HealthInfo).IsRequired().HasMaxLength(Constraints.LOW_VALUE_LENGTH);
        
        builder.HasMany(x => x.PetPhotos)
            .WithOne()
            .HasForeignKey(x=>x.PetId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.PaymentDetailsList)
            .WithOne()
            .HasForeignKey(x=>x.PetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}