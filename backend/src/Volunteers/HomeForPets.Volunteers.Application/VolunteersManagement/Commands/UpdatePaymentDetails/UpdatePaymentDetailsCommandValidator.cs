using FluentValidation;
using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.Core.Validation;
using HomeForPets.Volunteers.Domain.ValueObjects;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UpdatePaymentDetails;

public class UpdatePaymentDetailsCommandValidator : AbstractValidator<UpdatePaymentDetailsCommand>
{
    public UpdatePaymentDetailsCommandValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleForEach(u => u.PaymentDetailsDto).SetValidator(new PaymentDetailsCommandValidator());
    }
}

public class PaymentDetailsCommandValidator : AbstractValidator<PaymentDetailsDto>
{
    public PaymentDetailsCommandValidator()
    {
        RuleFor(u => u)
            .MustBeValueObject(f =>
                PaymentDetails.Create(f.Name, f.Description));
    }
}