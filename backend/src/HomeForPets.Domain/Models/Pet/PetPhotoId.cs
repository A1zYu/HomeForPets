namespace HomeForPets.Domain.Models.Pet;

public class PetPhotoId
{
    public Guid Value { get; }

    protected PetPhotoId(Guid id) => Value = id;
    public static PetPhotoId NewId() => new (Guid.NewGuid());
    public static PetPhotoId Empty() => new (Guid.Empty);

    public static PetPhotoId Create(Guid id) => new(id);
}