namespace HomeForPets.Application.Dtos.Volunteers;

public class PetsPhotoDto
{
    public Guid Id { get; init; }
    public Guid PetId { get; init; }
    public string Path { get; private set; } 
    public bool IsMain { get; private set; }
}