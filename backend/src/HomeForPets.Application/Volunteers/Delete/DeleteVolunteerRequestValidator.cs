using FluentValidation;
using HomeForPets.Application.Validation;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Application.Volunteers.Delete;

public class DeleteVolunteerRequestValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerRequestValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}