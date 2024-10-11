using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Abstaction;
using HomeForPets.Core.Extensions;
using HomeForPets.Volunteers.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UpdatePaymentDetails;

public class UpdatePaymentDetailsHandler : ICommandHandler<Guid,UpdatePaymentDetailsCommand>
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
        
        var paymentDetails = command.PaymentDetailsDto
            .Select(x=>PaymentDetails
                .Create(x.Name, x.Description).Value);
        volunteerResult.Value.AddPaymentDetails(paymentDetails);
        
        _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation(
            "Volunteer update payment details with id {volunteerId}",
            command.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }
}