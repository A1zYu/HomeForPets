namespace HomeForPets.Core.Ids;

public record PetPhotoId
{
    public Guid Value { get; }

    private PetPhotoId(Guid id) => Value = id;
    public static PetPhotoId NewId() => new(Guid.NewGuid());
    public static PetPhotoId Empty() => new(Guid.Empty);

    public static PetPhotoId Create(Guid id) => new(id);

    public static implicit operator PetPhotoId(Guid petPhotoId) => new(petPhotoId);

    public static implicit operator Guid(PetPhotoId petPhotoId)
    {
        ArgumentNullException.ThrowIfNull(petPhotoId);
        return petPhotoId.Value;
    }
}