using System.Xml.Linq;
using FluentValidation;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Validation;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

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