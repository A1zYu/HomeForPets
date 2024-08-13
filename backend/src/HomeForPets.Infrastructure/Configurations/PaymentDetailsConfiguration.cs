using HomeForPets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeForPets.Configurations;

public class PaymentDetailsConfiguration : IEntityTypeConfiguration<PaymentDetails>
{
    public void Configure(EntityTypeBuilder<PaymentDetails> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(Constraints.Constraints.HIGH_VALUE_LENGTH);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(Constraints.Constraints.HIGH_VALUE_LENGTH);
    }
}