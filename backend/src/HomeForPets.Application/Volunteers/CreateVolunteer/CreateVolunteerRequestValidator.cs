using FluentValidation;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Validation;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(x => x.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(x => x.FullNameDto)
            .MustBeValueObject(x => 
                FullName.Create(x.FirstName, x.LastName, x.MiddleName));
        RuleFor(x => x.Description).MustBeValueObject(Description.Create);
        RuleForEach(x => x.SocialNetworks)
            .MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Path));
        RuleForEach(x => x.PaymentDetailsList)
            .MustBeValueObject(x => SocialNetwork.Create(x.Name, x.Description));
    }
}