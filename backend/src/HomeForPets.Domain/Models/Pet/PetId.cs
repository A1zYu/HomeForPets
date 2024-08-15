namespace HomeForPets.Domain.Models.Pet;

public class PetId
{
    public Guid Value { get; }

    protected PetId(Guid id) => Value = id;
    public static PetId NewId() => new (Guid.NewGuid());
    public static PetId Empty() => new (Guid.Empty);

    public static PetId Create(Guid id) => new(id);
}