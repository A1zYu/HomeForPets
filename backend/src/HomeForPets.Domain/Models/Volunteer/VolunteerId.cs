namespace HomeForPets.Domain.Models.Volunteer;

public class VolunteerId
{
    public Guid Value { get; }

    protected VolunteerId(Guid id) => Value = id;
    public static VolunteerId NewId() => new (Guid.NewGuid());
    public static VolunteerId Empty() => new (Guid.Empty);

    public static VolunteerId Create(Guid id) => new(id);
}