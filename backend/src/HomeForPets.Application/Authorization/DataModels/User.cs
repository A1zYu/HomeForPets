using Microsoft.AspNetCore.Identity;

namespace HomeForPets.Application.Authorization.DataModels;

public class User : IdentityUser<Guid>
{
    public List<SocialNetwork> SocialNetworks { get; set; } = [];
}

public class SocialNetwork
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}