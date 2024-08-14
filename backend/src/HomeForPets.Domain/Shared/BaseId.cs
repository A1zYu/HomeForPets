namespace HomeForPets.Shared;

public class BaseId<T>
{
    public Guid Value { get; }

    protected BaseId(Guid id) => Value = id;
    
    public static BaseId<T> NewId() => new BaseId<T>(Guid.NewGuid());
    public static BaseId<T> Empty() => new BaseId<T>(Guid.Empty);

    public static T Create(Guid id) => (T)Activator.CreateInstance(typeof(T),id)!;

}