using Microsoft.AspNetCore.Identity;

namespace HomeForPets.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    public List<SocialNetwork> SocialNetworks { get; set; } = [];
}

public class SocialNetwork
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}