using HomeForPets.Shared;

namespace HomeForPets.Models;

public class SocialNetwork(string name, string path) : Entity<Guid>(Guid.NewGuid())
{
    public string Name { get; private set; } = name;
    public string Path { get; private set; } = path;
}