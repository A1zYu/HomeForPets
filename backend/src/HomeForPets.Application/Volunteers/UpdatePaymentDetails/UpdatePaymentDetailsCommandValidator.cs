using FluentValidation;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Validation;
using HomeForPets.Application.Volunteers.UpdateSocialNetworks;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;

namespace HomeForPets.Application.Volunteers.UpdatePaymentDetails;

public class UpdatePaymentDetailsCommandValidator : AbstractValidator<UpdatePaymentDetailsCommand>
{
    public UpdatePaymentDetailsCommandValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleFor(u => u.PaymentDetailsDto).SetValidator(new PaymentDetailsCommandValidator());
    }
}

public class PaymentDetailsCommandValidator : AbstractValidator<IEnumerable<PaymentDetailsDto>>
{
    public PaymentDetailsCommandValidator()
    {
        RuleForEach(u => u)
            .MustBeValueObject(f =>
                PaymentDetails.Create(f.Name, f.Description));
    }
}