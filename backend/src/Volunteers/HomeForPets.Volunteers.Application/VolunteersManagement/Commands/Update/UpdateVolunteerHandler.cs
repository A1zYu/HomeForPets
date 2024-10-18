using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Abstactions;
using HomeForPets.Core.Extensions;
using HomeForPets.SharedKernel;
using HomeForPets.Volunteers.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.Update;

public class UpdateVolunteerHandler : ICommandHandler<Guid,UpdateMainInfoCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateMainInfoCommand> _validator;

    public UpdateVolunteerHandler(IVolunteersRepository volunteersRepository, ILogger<UpdateVolunteerHandler> logger, IUnitOfWork unitOfWork, IValidator<UpdateMainInfoCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateMainInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validatorResult =await _validator.ValidateAsync(command,cancellationToken);
        if (validatorResult.IsValid == false)
        {
           return validatorResult.ToErrorList();
        }
        
        var volunteerResult =await _volunteersRepository.GetById(command.VolunteerId,cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var newFullName = FullName.Create(
            command.FullNameDto.FirstName,
            command.FullNameDto.LastName,
            command.FullNameDto.MiddleName).Value;
        
        var newDescription = Description.Create(command.Description).Value;
        
        var newPhoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        
        var newWorkExperience = YearsOfExperience.Create(command.WorkExperience).Value;
        
        volunteerResult.Value
            .UpdateMainInfo(newFullName,newDescription,newWorkExperience,newPhoneNumber);
        
        _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Updated volunteer with id {volunteerId}",
            command.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }
}
