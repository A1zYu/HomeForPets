using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Validation;
using HomeForPets.SharedKernel;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.DeletePhotoPet;

public class DeleteFileFromPetValidator : AbstractValidator<DeletePhotoPetCommand>
{
    public DeleteFileFromPetValidator()
    {
        RuleFor(x => x.PetId).NotNull().WithError(Error.NullValue);
        RuleFor(x => x.PhotoId).NotNull().WithError(Error.NullValue);
        RuleFor(x => x.VolunteerId).NotNull().WithError(Error.NullValue);
    }
}