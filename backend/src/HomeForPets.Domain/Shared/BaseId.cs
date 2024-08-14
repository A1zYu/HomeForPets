namespace HomeForPets.Shared;

public class BaseId<T>
{
    public Guid Id { get; }

    protected BaseId(Guid id) => Id = id;
    
    public static BaseId<T> NewId() => new BaseId<T>(Guid.NewGuid());
    public static BaseId<T> Empty() => new BaseId<T>(Guid.Empty);

}