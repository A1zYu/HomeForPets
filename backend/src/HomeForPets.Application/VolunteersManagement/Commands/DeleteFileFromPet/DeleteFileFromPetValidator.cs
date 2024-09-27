using FluentValidation;
using HomeForPets.Application.Validation;
using HomeForPets.Application.VolunteersManagement.Commands.DeletePhotoFromPet;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Application.VolunteersManagement.Commands.DeleteFileFromPet;

public class DeleteFileFromPetValidator : AbstractValidator<DeleteFileFromPetCommand>
{
    public DeleteFileFromPetValidator()
    {
        RuleFor(x => x.PetId).NotNull().WithError(Error.NullValue);
        RuleFor(x => x.PhotoId).NotNull().WithError(Error.NullValue);
        RuleFor(x => x.VolunteerId).NotNull().WithError(Error.NullValue);
    }
}