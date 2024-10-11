namespace HomeForPets.Core.Dtos.Volunteers;

public class VolunteerDto
{
    public Guid Id { get; init; }
    public FullNameDto FullName { get; init; }
    public string PhoneNumber { get; init; }
    public int YearsOfExperience { get; init; }
    // public SocialNetworkDto[] SocialNetworks { get; init; }
    public PaymentDetailsDto[] PaymentDetails { get; init; }
    
}