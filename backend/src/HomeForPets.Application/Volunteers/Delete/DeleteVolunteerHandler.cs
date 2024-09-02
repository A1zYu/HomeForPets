using CSharpFunctionalExtensions;
using FluentValidation.Validators;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.Volunteers.Delete;

public class DeleteVolunteerHandler
{
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;

    public DeleteVolunteerHandler(ILogger<DeleteVolunteerHandler> logger, IVolunteersRepository volunteersRepository)
    {
        _logger = logger;
         _volunteersRepository= volunteersRepository;
    }
    public async Task<Result<Guid, Error>> Handle(
        DeleteVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult =await _volunteersRepository.GetById(request.VolunteerId,cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var result =await _volunteersRepository.Delete(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation(
            "Volunteer deleted with id {volunteerId}",
            request.VolunteerId);
        
        return result;
    }
}