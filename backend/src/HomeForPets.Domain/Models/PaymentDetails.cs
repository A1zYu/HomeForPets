namespace HomeForPets.Models;

public class PaymentDetails
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid? PetId { get; private set; }
    public Guid? VolunteerId { get; private set; }
}