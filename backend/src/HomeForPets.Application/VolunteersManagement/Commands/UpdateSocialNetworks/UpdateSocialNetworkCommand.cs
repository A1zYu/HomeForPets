using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.VolunteersManagement.Commands.UpdateSocialNetworks;

public record UpdateSocialNetworkCommand(Guid VolunteerId,IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;