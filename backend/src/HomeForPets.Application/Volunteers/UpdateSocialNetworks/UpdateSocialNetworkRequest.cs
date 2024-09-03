using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.Volunteers.UpdateSocialNetworks;

public record UpdateSocialNetworkRequest(Guid VolunteerId,UpdateSocialNetworksDto SocialNetworksDto);
public record UpdateSocialNetworksDto(IEnumerable<SocialNetworkDto> SocialNetworks);