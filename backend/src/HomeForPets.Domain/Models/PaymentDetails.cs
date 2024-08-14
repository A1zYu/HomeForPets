using HomeForPets.Shared;

namespace HomeForPets.Models;

public class PaymentDetails (string name , string description) : Entity<Guid>(Guid.NewGuid())
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
}