using HomeForPets.Species.Application.SpeciesManagement.Commands.CreateSpecies;

namespace HomeForPets.Species.Controllers.Request;

public record CreateSpeciesRequest(string Name)
{
    public CreateSpeciesCommand ToCommand() => new (Name);
};