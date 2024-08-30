namespace HomeForPets.Domain.Shared.Ids;

public class VolunteerId
{
    public Guid Value { get; }

    protected VolunteerId(Guid id) => Value = id;
    public static VolunteerId NewId() => new (Guid.NewGuid());
    public static VolunteerId Empty() => new (Guid.Empty);

    public static VolunteerId Create(Guid id) => new(id);
    public static implicit operator VolunteerId(Guid id) => new(id);
    public static implicit operator Guid(VolunteerId volunteerId)
    {
        ArgumentNullException.ThrowIfNull(volunteerId);
        return volunteerId.Value;
    }
}