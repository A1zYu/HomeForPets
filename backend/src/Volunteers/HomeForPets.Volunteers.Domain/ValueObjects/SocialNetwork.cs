using CSharpFunctionalExtensions;
using HomeForPets.Core;

namespace HomeForPets.Volunteers.Domain.ValueObjects;

public record SocialNetworkList
{
    private SocialNetworkList() { }
    private SocialNetworkList(IEnumerable<SocialNetwork> list) => SocialNetworks = list.ToList();
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; } = default!;
    public static SocialNetworkList Create(IEnumerable<SocialNetwork> list) => new(list);
}
public record SocialNetwork
{
    private SocialNetwork() { }
    private SocialNetwork(string name, string path)
    {
        Name = name;
        Path = path;
    }
    public string Name { get; private set; }
    public string Path { get; private set; }

    public static Result<SocialNetwork,Error> Create(string name, string path)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.LOW_VALUE_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Social network name");
        }

        if (string.IsNullOrWhiteSpace(path))
        {
            return Errors.General.ValueIsInvalid("Social network path");
        }
        var socialNetwork = new SocialNetwork(name, path);
        return socialNetwork;
    }
}