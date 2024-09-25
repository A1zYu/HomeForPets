using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.Volunteers;
using HomeForPets.Application.VolunteersManagement.Commands.UpdateSocialNetworks;

namespace HomeForPets.Api.Controllers.Volunteers.Request;

public record UpdateSocialNetworksRequest(IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public UpdateSocialNetworkCommand ToCommand(Guid id) => new(id, SocialNetworks);
}