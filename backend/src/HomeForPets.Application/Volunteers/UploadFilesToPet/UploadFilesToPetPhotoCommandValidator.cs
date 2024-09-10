using FluentValidation;
using HomeForPets.Application.Dtos.Validators;
using HomeForPets.Application.Validation;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Application.Volunteers.UploadFilesToPet;

public class UploadFilesToPetPhotoCommandValidator : AbstractValidator<UploadFilesToPetPhotoCommand>
{
    public UploadFilesToPetPhotoCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(u => u.Files).SetValidator(new UploadFileDtoValidator());        
    }
}