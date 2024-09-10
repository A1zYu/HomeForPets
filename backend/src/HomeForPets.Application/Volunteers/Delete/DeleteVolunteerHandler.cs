using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Validators;
using HomeForPets.Application.Database;
using HomeForPets.Application.Extensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.Volunteers.Delete;

public class DeleteVolunteerHandler
{
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteVolunteerCommand> _validator;

    public DeleteVolunteerHandler(ILogger<DeleteVolunteerHandler> logger, IVolunteersRepository volunteersRepository, IUnitOfWork unitOfWork, IValidator<DeleteVolunteerCommand> validator)
    {
        _logger = logger;
         _volunteersRepository= volunteersRepository;
         _unitOfWork = unitOfWork;
         _validator = validator;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validatorResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validatorResult.IsValid == false)
        {
            return validatorResult.ToErrorList();
        }
        
        var volunteerResult =await _volunteersRepository.GetById(command.VolunteerId,cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        volunteerResult.Value.Delete();
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation(
            "Volunteer deleted with id {volunteerId}",
            command.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }
}