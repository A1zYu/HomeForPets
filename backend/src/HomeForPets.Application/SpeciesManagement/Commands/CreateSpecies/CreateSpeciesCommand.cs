using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.SpeciesManagement.Commands.CreateSpecies;

public record CreateSpeciesCommand(string Name) : ICommand;