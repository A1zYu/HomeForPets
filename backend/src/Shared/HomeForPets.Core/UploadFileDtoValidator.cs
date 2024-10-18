using FluentValidation;
using HomeForPets.Core.FilesDto;
using HomeForPets.Core.Validation;
using HomeForPets.SharedKernel;

namespace HomeForPets.Core;

public class UploadFileDtoValidator: AbstractValidator<UploadFileDto>
{
    public UploadFileDtoValidator()
    {
        RuleFor(u => u.FileName)
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.Content)
            .Must(c => c.Length < 5000000);
    }
}