using HomeForPets.Application.SpeciesManagement.Commands.DeleteBreedToSpecies;

namespace HomeForPets.Api.Controllers.Species.Request;

public record DeleteBreedRequest(Guid BreedId)
{
    public DeleteBreedToSpeciesCommand ToCommand(Guid speciesId) => new(speciesId, BreedId);
};