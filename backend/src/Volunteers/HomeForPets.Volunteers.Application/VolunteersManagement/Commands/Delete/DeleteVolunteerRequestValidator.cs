using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Validation;
using HomeForPets.SharedKernel;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.Delete;

public class DeleteVolunteerRequestValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerRequestValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}