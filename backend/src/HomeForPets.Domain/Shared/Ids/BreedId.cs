namespace HomeForPets.Domain.Shared.Ids;

public record BreedId
{
    public Guid Value { get; }

    private BreedId(Guid id) => Value = id;
    public static BreedId NewId() => new (Guid.NewGuid());
    public static BreedId Empty() => new (Guid.Empty);

    public static BreedId Create(Guid id) => new(id);
}