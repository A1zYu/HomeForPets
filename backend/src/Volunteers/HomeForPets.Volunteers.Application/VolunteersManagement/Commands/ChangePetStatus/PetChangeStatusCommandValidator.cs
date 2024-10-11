using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Validation;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.ChangePetStatus;

public class PetChangeStatusCommandValidator : AbstractValidator<PetChangeStatusCommand>
{
    public PetChangeStatusCommandValidator()
    {
        RuleFor(x => x.PetId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("PetId"));
        RuleFor(x => x.VolunteerId)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("VolunteerId"));
    }
}