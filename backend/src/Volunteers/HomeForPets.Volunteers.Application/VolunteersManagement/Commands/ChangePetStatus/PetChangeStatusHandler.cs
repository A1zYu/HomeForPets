using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Abstaction;
using HomeForPets.Core.Extensions;
using HomeForPets.Core.Ids;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.ChangePetStatus;

public class PetChangeStatusHandler : ICommandHandler<PetChangeStatusCommand>
{
    private readonly IValidator<PetChangeStatusCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PetChangeStatusHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;
    
    public PetChangeStatusHandler(IValidator<PetChangeStatusCommand> validator, IVolunteersRepository volunteersRepository, ILogger<PetChangeStatusHandler> logger, IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(PetChangeStatusCommand command, CancellationToken ct)
    {
        var validatorResult = await _validator.ValidateAsync(command,ct);
        if (!validatorResult.IsValid)
        {
            return validatorResult.ToErrorList();
        }

        var volunteer = await _volunteersRepository.GetById(command.VolunteerId,ct);
        if (volunteer.IsFailure)
        {
            return volunteer.Error.ToErrorList();
        }
        var petId = PetId.Create(command.PetId);

        var result = volunteer.Value.SetPetStatus(petId, command.HelpStatus);

        if (result.IsFailure)
        {
            return result.Error.ToErrorList();
        }

        await _unitOfWork.SaveChanges(ct);
        
        
        
        return UnitResult.Success<ErrorList>();
    }
}