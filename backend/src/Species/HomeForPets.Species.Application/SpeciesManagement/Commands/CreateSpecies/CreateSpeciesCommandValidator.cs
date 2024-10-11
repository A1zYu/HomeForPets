using FluentValidation;
using HomeForPets.Core;

namespace HomeForPets.Species.Application.SpeciesManagement.Commands.CreateSpecies;

public class CreateSpeciesCommandValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesCommandValidator()
    {
        RuleFor(species => species.Name)
            .NotEmpty()
            .MaximumLength(Constants.LOW_VALUE_LENGTH);
    }
}