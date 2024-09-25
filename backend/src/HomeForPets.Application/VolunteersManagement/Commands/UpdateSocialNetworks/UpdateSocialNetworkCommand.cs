using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.Volunteers;

namespace HomeForPets.Application.VolunteersManagement.Commands.UpdateSocialNetworks;

public record UpdateSocialNetworkCommand(Guid VolunteerId,IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;