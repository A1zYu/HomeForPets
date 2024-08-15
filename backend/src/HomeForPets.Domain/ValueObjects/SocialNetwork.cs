using CSharpFunctionalExtensions;

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

    public static Result<SocialNetwork> Create(string name, string path)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<SocialNetwork>("Name is not empty");
        }

        if (string.IsNullOrWhiteSpace(path))
        {
            return Result.Failure<SocialNetwork>("Path is not empty");
        }
        var socialNetwork = new SocialNetwork(name, path);
        return Result.Success(socialNetwork);
    }
}