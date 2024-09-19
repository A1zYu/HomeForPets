using System.Text.Json;
using HomeForPets.Application.Dtos;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HomeForPets.Infrastructure.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");
        builder.HasKey(x => x.Id);
        
        builder.OwnsOne(x => x.FullName, fullname =>
        {
            fullname.Property(f => f.FirstName).HasColumnName("first_name");
            fullname.Property(f => f.LastName).HasColumnName("last_name");
            fullname.Property(f => f.MiddleName).HasColumnName("middle_name");
        });
        
        builder.Property(v => v.PaymentDetails).HasConversion(
            details => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
            json => JsonSerializer.Deserialize<PaymentDetailsDto[]>(json, JsonSerializerOptions.Default)!
        );
    }
}