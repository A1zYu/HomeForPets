using HomeForPets.Core.Abstaction;

namespace HomeForPets.Species.Application.SpeciesManagement.Commands.DeleteBreedToSpecies;

public record DeleteBreedToSpeciesCommand(Guid SpeciesId,Guid BreedId) : ICommand;