using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Abstactions;
using HomeForPets.Core.Extensions;
using HomeForPets.SharedKernel;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.Delete;

public class DeleteVolunteerHandler : ICommandHandler<Guid,DeleteVolunteerCommand>
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