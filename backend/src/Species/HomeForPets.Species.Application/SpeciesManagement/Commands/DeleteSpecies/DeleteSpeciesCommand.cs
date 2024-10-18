using HomeForPets.Core.Abstactions;

namespace HomeForPets.Species.Application.SpeciesManagement.Commands.DeleteSpecies;

public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;