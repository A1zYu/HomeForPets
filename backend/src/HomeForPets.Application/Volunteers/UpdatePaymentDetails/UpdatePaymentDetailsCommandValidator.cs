using FluentValidation;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Validation;
using HomeForPets.Application.Volunteers.UpdateSocialNetworks;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;

namespace HomeForPets.Application.Volunteers.UpdatePaymentDetails;

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