namespace HomeForPets.Models;

public class SocialNetwork
{
    public Guid Id { get;private set; }
    public Guid? VolunteerId { get; private set; }
    public string Name { get; private set; }
    public string Path { get; private set; }
}