using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Validation;
using HomeForPets.Volunteers.Domain.ValueObjects;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.CreateVolunteer;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);
        RuleFor(x => x.WorkExperience)
            .Must(x => x >= 0)
            .WithError(Errors.General.ValueIsRequired("work experience"));
        RuleFor(x => x.FullNameDto)
            .MustBeValueObject(x => 
                FullName.Create(x.FirstName, x.LastName, x.MiddleName));
        RuleFor(x => x.Description)
            .MustBeValueObject(Description.Create);
        RuleForEach(x => x.PaymentDetails)
            .MustBeValueObject(p => PaymentDetails.Create(p.Name, p.Description));
        RuleForEach(x => x.SocialNetworks)
            .MustBeValueObject(p => SocialNetwork.Create(p.Name, p.Path));

    }
}