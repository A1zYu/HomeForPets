using HomeForPets.Species.Application.SpeciesManagement.Commands.AddBreed;

namespace HomeForPets.Species.Controllers.Request;

public record AddBreedRequest(string Name)
{
    public AddBreedCommand ToCommand(Guid speciesId) => new (speciesId,Name);
}