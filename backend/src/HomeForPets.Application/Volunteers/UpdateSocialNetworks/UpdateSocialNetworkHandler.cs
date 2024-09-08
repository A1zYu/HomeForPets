using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Application.Database;
using HomeForPets.Application.Extensions;
using HomeForPets.Application.Validation;
using HomeForPets.Application.Volunteers.Delete;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Volunteers;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworkHandler
{
    private readonly ILogger<UpdateSocialNetworkHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<UpdateSocialNetworkCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSocialNetworkHandler(ILogger<UpdateSocialNetworkHandler> logger, IVolunteersRepository volunteersRepository, IUnitOfWork unitOfWork, IValidator<UpdateSocialNetworkCommand> validator)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateSocialNetworkCommand command,
        CancellationToken cancellationToken = default)
    {
        var validatorResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validatorResult.IsValid==false)
        {
            return validatorResult.ToErrorList();
        }
        
        var volunteerResult =await _volunteersRepository.GetById(command.VolunteerId,cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var socialNetworks = SocialNetworkList.Create(command.SocialNetworks
            .Select(x => SocialNetwork.Create(x.Name, x.Path).Value));
        
        volunteerResult.Value.AddSocialNetworks(socialNetworks);
        
        _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation(
            "Volunteer update social networks with id {volunteerId}",
            command.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }
}