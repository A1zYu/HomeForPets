using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.SpeciesManagement.Commands.AddBreed;

public record AddBreedCommand(Guid SpeciesId, string Name) : ICommand;