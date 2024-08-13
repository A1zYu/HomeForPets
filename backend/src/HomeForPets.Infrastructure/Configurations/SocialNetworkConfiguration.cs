using HomeForPets.Domain.Constraints;
using HomeForPets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Configurations;

public class SocialNetworkConfiguration : IEntityTypeConfiguration<SocialNetwork>
{
    public void Configure(EntityTypeBuilder<SocialNetwork> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(Constraints.HIGH_VALUE_LENGTH);
        builder.Property(p => p.Path).IsRequired().HasMaxLength(Constraints.HIGH_VALUE_LENGTH);
    }
}