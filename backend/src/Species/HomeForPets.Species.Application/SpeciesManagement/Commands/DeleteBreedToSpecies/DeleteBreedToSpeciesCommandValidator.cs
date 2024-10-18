using FluentValidation;

namespace HomeForPets.Species.Application.SpeciesManagement.Commands.DeleteBreedToSpecies;

public class DeleteBreedToSpeciesCommandValidator : AbstractValidator<DeleteBreedToSpeciesCommand>
{
    public DeleteBreedToSpeciesCommandValidator()
    {
        RuleFor(x => x.BreedId).NotNull();
        RuleFor(x => x.SpeciesId).NotNull();
    }
}