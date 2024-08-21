using FluentValidation;
using HomeForPets.Application.Validation;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(x => x.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(x => new { x.FirstName, x.LastName, x.MiddleName })
            .MustBeValueObject(x => 
                FullName.Create(x.FirstName, x.LastName, x.MiddleName));
    }
}