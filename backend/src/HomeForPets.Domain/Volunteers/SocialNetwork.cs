using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Domain.Volunteers;

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
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constraints.Constraints.LOW_VALUE_LENGTH)
        {
            return Errors.General.Validation("Social network name");
        }

        if (string.IsNullOrWhiteSpace(path))
        {
            return Errors.General.Validation("Social network path");
        }
        var socialNetwork = new SocialNetwork(name, path);
        return socialNetwork;
    }
}