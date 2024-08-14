using HomeForPets.Shared;

namespace HomeForPets.Models;

public class PetPhoto (string path, bool isMain) : Entity<Guid>(Guid.NewGuid())
{
    public string Path { get; private set; } = path;
    public bool IsMain { get; private set; } = isMain;
}