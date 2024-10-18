using HomeForPets.Core.Abstactions;

namespace HomeForPets.Species.Application.SpeciesManagement.Commands.AddBreed;

public record AddBreedCommand(Guid SpeciesId, string Name) : ICommand;