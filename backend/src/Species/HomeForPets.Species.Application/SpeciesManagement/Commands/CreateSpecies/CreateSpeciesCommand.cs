using HomeForPets.Core.Abstactions;

namespace HomeForPets.Species.Application.SpeciesManagement.Commands.CreateSpecies;

public record CreateSpeciesCommand(string Name) : ICommand;