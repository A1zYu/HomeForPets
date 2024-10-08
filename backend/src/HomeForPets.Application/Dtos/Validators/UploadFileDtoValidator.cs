﻿using FluentValidation;
using HomeForPets.Application.Dtos.FilesDto;
using HomeForPets.Application.Validation;
using HomeForPets.Domain.Shared;
using Microsoft.VisualBasic;
using Constants = HomeForPets.Domain.Constraints.Constants;

namespace HomeForPets.Application.Dtos.Validators;

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