using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Validation;
using HomeForPets.Volunteers.Domain.ValueObjects;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.Update;

public class UpdateMainInfoCommandValidator : AbstractValidator<UpdateMainInfoCommand>
{
    public UpdateMainInfoCommandValidator()
    {
        RuleFor(r => r.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(x => x.FullNameDto)
            .MustBeValueObject(x => 
                FullName.Create(x.FirstName, x.LastName, x.MiddleName));
        RuleFor(r => r.Description).MustBeValueObject(Description.Create);
        RuleFor(r => r.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(r => r.WorkExperience).MustBeValueObject(YearsOfExperience.Create);
    }
}