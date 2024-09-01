namespace HomeForPets.Domain.Shared.Ids;

public record PetId
{
    public Guid Value { get; }

    private PetId(Guid id) => Value = id;
    public static PetId NewId() => new (Guid.NewGuid());
    public static PetId Empty() => new (Guid.Empty);
    public static implicit operator PetId(Guid id) => new(id);
    public static implicit operator Guid(PetId petId)
    {
        ArgumentNullException.ThrowIfNull(petId);

        return petId.Value;
    }

    public static PetId Create(Guid id) => new(id);
}