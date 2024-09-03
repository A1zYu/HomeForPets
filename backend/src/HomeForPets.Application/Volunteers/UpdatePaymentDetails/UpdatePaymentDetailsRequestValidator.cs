using FluentValidation;
using HomeForPets.Application.Validation;
using HomeForPets.Application.Volunteers.UpdateSocialNetworks;
using HomeForPets.Domain.Volunteers;

namespace HomeForPets.Application.Volunteers.UpdatePaymentDetails;

public class UpdatePaymentDetailsRequestValidator : AbstractValidator<UpdatePaymentDetailsRequest>
{
    public UpdatePaymentDetailsRequestValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
    }
}
public class UpdateSocialNetworksDtoValidator : AbstractValidator<UpdatePaymentDetailsDto>
{
    public UpdateSocialNetworksDtoValidator()
    {
        RuleForEach(u => u.PaymentDetails)
            .MustBeValueObject(f => 
                SocialNetwork.Create(f.Name, f.Description));
    }
}