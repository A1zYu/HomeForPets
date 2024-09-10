using HomeForPets.Application.Dtos;
using HomeForPets.Application.Volunteers.UpdateSocialNetworks;

namespace HomeForPets.Api.Controllers.Volunteers.Request;

public record UpdateSocialNetworksRequest(IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public UpdateSocialNetworkCommand ToCommand(Guid id) => new(id, SocialNetworks);
}