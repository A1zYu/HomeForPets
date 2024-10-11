using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Validation;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UploadFilesToPet;

public class UploadFilesToPetPhotoCommandValidator : AbstractValidator<UploadFilesToPetPhotoCommand>
{
    public UploadFilesToPetPhotoCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(u => u.Files).SetValidator(new UploadFileDtoValidator());        
    }
}