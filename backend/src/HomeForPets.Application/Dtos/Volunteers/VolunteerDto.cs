namespace HomeForPets.Application.Dtos.Volunteers;

public class VolunteerDto
{
    public Guid Id { get; init; }
    public FullNameDto FullName { get; init; }
    public string PhoneNumber { get; init; }
    public int YearsOfExperience { get; init; }
    // public SocialNetworkDto[] SocialNetworks { get; init; }
    public PaymentDetailsDto[] PaymentDetails { get; init; }
    
}

public class PetDto
{
    public Guid Id { get; init; }
    public Guid VolunteerId { get; init; }
    public Guid SpeciesId { get; init; } 
    public Guid BreedId { get; init; } 
    public PetsPhoto[] PetPhotos { get; init; }
    
}

public class PetsPhoto
{
    public Guid Id { get; init; }
    public Guid PetId { get; init; }
    public string Path { get; private set; } 
    public bool IsMain { get; private set; }
}