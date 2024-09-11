using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Application.Database;
using HomeForPets.Application.Extensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.VolunteersManagement;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVolunteerCommand> _validator;

    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository, ILogger<CreateVolunteerHandler> logger, IUnitOfWork unitOfWork, IValidator<CreateVolunteerCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var validatorResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validatorResult.IsValid == false)
        {
            return validatorResult.ToErrorList();
        }
        
        var volunteerId = VolunteerId.NewId();
        
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        
        var fullname = FullName.Create(command.FullNameDto.FirstName, command.FullNameDto.LastName,
            command.FullNameDto.MiddleName).Value;
        
        var description = Description.Create(command.Description).Value;
        
        var yearsOfExperience = YearsOfExperience.Create(command.WorkExperience).Value;
        
        var existVolunteerByPhone = await _volunteersRepository.GetByPhoneNumber(phoneNumber);
        if (existVolunteerByPhone is not null)
            return Errors.Volunteer.AlreadyExist().ToErrorList();
        
        var volunteer = Volunteer.Create(
            volunteerId,
            fullname,
            phoneNumber,
            description,
            yearsOfExperience
        );
        if (volunteer.IsFailure)
        {
            return volunteer.Error.ToErrorList();
        }

        if (command.SocialNetworks.Any())
        {
            var socialNetworks = SocialNetworkList.Create(command.SocialNetworks
                .Select(x => SocialNetwork.Create(x.Name, x.Path).Value));
            volunteer.Value.AddSocialNetworks(socialNetworks);
        }

        if (command.PaymentDetails.Any())
        {
            var paymentDetails = PaymentDetailsList.Create(command.PaymentDetails
                .Select(x=>PaymentDetails.Create(x.Name,x.Description).Value));
            volunteer.Value.AddPaymentDetails(paymentDetails);
        }
        _logger.LogInformation("Create volunteer : {volunteerId} ", volunteerId.Value); 
        
        await _volunteersRepository.Add(volunteer.Value, cancellationToken);
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        return volunteerId.Value;
    }
}