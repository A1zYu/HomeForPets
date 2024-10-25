using FluentValidation;

namespace HomeForPets.Application.SpeciesManagement.Commands.DeleteSpecies;

public class DeleteSpeciesCommandValidator : AbstractValidator<DeleteSpeciesCommand>
{
    public DeleteSpeciesCommandValidator()
    {
        RuleFor(species => species.SpeciesId).NotNull();
    }
}