using HomeForPets.Application.SpeciesManagement.Commands.CreateSpecies;

namespace HomeForPets.Api.Controllers.Species.Request;

public record CreateSpeciesRequest(string Name)
{
    public CreateSpeciesCommand ToCommand() => new (Name);
};