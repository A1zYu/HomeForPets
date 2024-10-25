using HomeForPets.Domain.VolunteersManagement.Enums;

namespace HomeForPets.Application.Dtos.Volunteers;

public class PetDto
{
    public Guid Id { get; init; }
    public Guid VolunteerId { get; init; }
    public int Position { get; init; }
    public HelpStatus helpStatus { get; init; }
    
    public string PhoneNumber { get; init; }
    
    public string Name { get; init; }
    public string Color { get; init; }
    public string City { get; init; }

    public Guid SpeciesId { get; init; }

    // public string SpeciesName { get; init; }
    public Guid BreedId { get; init; }

    // public string BreedName { get; init; }
    public IReadOnlyList<PetsPhotoDto> PetPhotos { get; init; }
}