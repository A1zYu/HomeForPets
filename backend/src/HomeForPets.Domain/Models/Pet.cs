using HomeForPets.Enums;

namespace HomeForPets.Models;

public class Pet
{
    private readonly List<PaymentDetails> _requisites = [];
    
    public Guid Id { get; private set; }

    public string Name { get; private set; } = default!;

    public string Species { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public string Breed { get; private set; } = default!;

    public string Color { get; private set; } = default!;

    public string HealthInfo { get; private set; } = default!;

    public string Address { get; private set; } = default!;

    public double Weight { get; private set; }
    
    public double Height { get; private set; }

    public string PhoneNumber { get; private set; } = default!;
    
    public bool IsNeutered { get; private set; }
    
    public DateOnly BirthOfDate { get; private set; }
    
    public bool IsVaccinated { get; private set; }
    
    public HelpStatus HelpStatus { get; private set; }

    public IReadOnlyList<PaymentDetails> Requisites => _requisites;
    
    public DateOnly CreatedDate { get; private set; }

}