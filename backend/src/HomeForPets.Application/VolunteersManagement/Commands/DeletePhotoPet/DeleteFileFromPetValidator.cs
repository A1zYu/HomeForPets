using FluentValidation;
using HomeForPets.Application.Validation;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Application.VolunteersManagement.Commands.DeletePhotoPet;

public class DeleteFileFromPetValidator : AbstractValidator<DeletePhotoPetCommand>
{
    public DeleteFileFromPetValidator()
    {
        RuleFor(x => x.PetId).NotNull().WithError(Error.NullValue);
        RuleFor(x => x.PhotoId).NotNull().WithError(Error.NullValue);
        RuleFor(x => x.VolunteerId).NotNull().WithError(Error.NullValue);
    }
}