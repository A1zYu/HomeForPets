using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Domain.ValueObjects;

public record SocialNetwork
{
    private SocialNetwork(string name, string path)
    {
        Name = name;
        Path = path;
    }
    public string Name { get; private set; }
    public string Path { get; private set; }

    public static Result<SocialNetwork,Error> Create(string name, string path)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Errors.General.Validation("Socail network name");
        }

        if (string.IsNullOrWhiteSpace(path))
        {
            return Errors.General.Validation("Payment detail path");
        }
        var socialNetwork = new SocialNetwork(name, path);
        return socialNetwork;
    }
}