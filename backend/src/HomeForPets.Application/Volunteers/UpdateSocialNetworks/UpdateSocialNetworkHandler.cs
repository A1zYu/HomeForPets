using CSharpFunctionalExtensions;
using HomeForPets.Application.Volunteers.Delete;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Volunteers;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworkHandler
{
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;

    public UpdateSocialNetworkHandler(ILogger<DeleteVolunteerHandler> logger, IVolunteersRepository volunteersRepository)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
    }
    public async Task<Result<Guid, Error>> Handle(
        UpdateSocialNetworkRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult =await _volunteersRepository.GetById(request.VolunteerId,cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var socialNetworks = SocialNetworkList.Create(request.SocialNetworksDto.SocialNetworks
            .Select(x => SocialNetwork.Create(x.Name, x.Path).Value));
        volunteerResult.Value.AddSocialNetworks(socialNetworks);
        var result =await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation(
            "Volunteer update social networks with id {volunteerId}",
            request.VolunteerId);
        
        return result;
    }
}