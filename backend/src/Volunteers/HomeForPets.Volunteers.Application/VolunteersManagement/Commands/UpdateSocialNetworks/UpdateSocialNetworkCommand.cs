using HomeForPets.Core.Abstactions;
using HomeForPets.Core.Dtos.Volunteers;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UpdateSocialNetworks;

public record UpdateSocialNetworkCommand(Guid VolunteerId,IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;