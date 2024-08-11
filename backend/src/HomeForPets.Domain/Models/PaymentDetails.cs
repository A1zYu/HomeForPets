namespace HomeForPets.Models;

public class PaymentDetails
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    private string Description { get; set; } = default!;
}