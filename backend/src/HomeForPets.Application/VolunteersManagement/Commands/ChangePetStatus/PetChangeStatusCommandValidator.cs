using FluentValidation;
using HomeForPets.Application.Validation;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.VolunteersManagement.Enums;

namespace HomeForPets.Application.VolunteersManagement.Commands.ChangePetStatus;

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