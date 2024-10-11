using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UpdateSocialNetworks;

namespace HomeForPets.Volunteers.Controllers.Request;

public record UpdateSocialNetworksRequest(IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public UpdateSocialNetworkCommand ToCommand(Guid id) => new(id, SocialNetworks);
}