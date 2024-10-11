using HomeForPets.Core.Abstaction;

namespace HomeForPets.Species.Application.SpeciesManagement.Commands.CreateSpecies;

public record CreateSpeciesCommand(string Name) : ICommand;