using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.Volunteers.UpdateSocialNetworks;

public record UpdateSocialNetworkCommand(Guid VolunteerId,IEnumerable<SocialNetworkDto> SocialNetworks);