using CSharpFunctionalExtensions;
using HomeForPets.Application.Volunteers.Delete;
using HomeForPets.Application.Volunteers.UpdateSocialNetworks;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.Volunteers.UpdatePaymentDetails;

public class UpdatePaymentDetailsHandler
{
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;

    public UpdatePaymentDetailsHandler(ILogger<DeleteVolunteerHandler> logger, IVolunteersRepository volunteersRepository)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
    }
    public async Task<Result<Guid, Error>> Handle(
        UpdatePaymentDetailsRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult =await _volunteersRepository.GetById(request.VolunteerId,cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var paymentDetails = PaymentDetailsList.Create(request.PaymentDetailsDto.PaymentDetails
            .Select(x => PaymentDetails.Create(x.Name, x.Description).Value));
        volunteerResult.Value.AddPaymentDetails(paymentDetails);
        var result =await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation(
            "Volunteer update payment details with id {volunteerId}",
            request.VolunteerId);
        
        return result;
    }
}