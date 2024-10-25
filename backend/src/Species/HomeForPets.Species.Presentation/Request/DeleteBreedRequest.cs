using HomeForPets.Species.Application.SpeciesManagement.Commands.DeleteBreedToSpecies;

namespace HomeForPets.Species.Controllers.Request;

public record DeleteBreedRequest(Guid BreedId)
{
    public DeleteBreedToSpeciesCommand ToCommand(Guid speciesId) => new(speciesId, BreedId);
};