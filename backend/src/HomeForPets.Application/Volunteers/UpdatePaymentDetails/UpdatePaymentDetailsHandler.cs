using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Application.Database;
using HomeForPets.Application.Extensions;
using HomeForPets.Application.Validation;
using HomeForPets.Application.Volunteers.Delete;
using HomeForPets.Application.Volunteers.UpdateSocialNetworks;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.Volunteers.UpdatePaymentDetails;

public class UpdatePaymentDetailsHandler
{
    private readonly ILogger<UpdatePaymentDetailsHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePaymentDetailsCommand> _validator;

    public UpdatePaymentDetailsHandler(
        ILogger<UpdatePaymentDetailsHandler> logger,
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork, 
        IValidator<UpdatePaymentDetailsCommand> validator)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdatePaymentDetailsCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerResult =await _volunteersRepository.GetById(command.VolunteerId,cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var paymentDetails = PaymentDetailsList.Create(command.PaymentDetailsDto
            .Select(x => PaymentDetails.Create(x.Name, x.Description).Value));
        
        volunteerResult.Value.AddPaymentDetails(paymentDetails);
        
        _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation(
            "Volunteer update payment details with id {volunteerId}",
            command.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }
}