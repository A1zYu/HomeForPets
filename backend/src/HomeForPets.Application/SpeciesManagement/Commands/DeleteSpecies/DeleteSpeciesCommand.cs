using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.SpeciesManagement.Commands.DeleteSpecies;

public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;