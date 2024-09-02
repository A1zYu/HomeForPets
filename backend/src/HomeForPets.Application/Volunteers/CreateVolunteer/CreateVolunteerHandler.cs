using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;

    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository, ILogger<CreateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.NewId();
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber).Value;
        var fullname = FullName.Create(request.FullNameDto.FirstName, request.FullNameDto.LastName,
            request.FullNameDto.MiddleName).Value;
        var description = Description.Create(request.Description).Value;
        var yearsOfExperience = YearsOfExperience.Create(request.WorkExperience).Value;
        
        
        var existVolunteerByPhone = await _volunteersRepository.GetByPhoneNumber(phoneNumber);
        if (existVolunteerByPhone is not null)
            return Errors.Volunteer.AlreadyExist();
        var volunteer = Volunteer.Create(
            volunteerId,
            fullname,
            phoneNumber,
            description,
            yearsOfExperience
        );
        if (volunteer.IsFailure)
        {
            return volunteer.Error;
        }

        if (request.SocialNetworks != null && request.SocialNetworks.Any())
        {
            var socialNetworks = SocialNetworkList.Create(request.SocialNetworks
                .Select(x => SocialNetwork.Create(x.Name, x.Path).Value));
            volunteer.Value.AddSocialNetworks(socialNetworks);
        }

        if (request.PaymentDetails != null && request.PaymentDetails.Any())
        {
            var paymentDetails = PaymentDetailsList.Create(request.PaymentDetails
                .Select(x=>PaymentDetails.Create(x.Name,x.Description).Value));
            volunteer.Value.AddPaymentDetails(paymentDetails);
        }
        _logger.LogInformation("Create volunteer : {volunteerId} ", volunteerId.Value);
        return await _volunteersRepository.Add(volunteer.Value, cancellationToken);
        ;
    }
}