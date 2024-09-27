using HomeForPets.Application.SpeciesManagement.Commands.AddBreed;

namespace HomeForPets.Api.Controllers.Species.Request;

public record AddBreedRequest(string Name)
{
    public AddBreedCommand ToCommand(Guid speciesId) => new (speciesId,Name);
}