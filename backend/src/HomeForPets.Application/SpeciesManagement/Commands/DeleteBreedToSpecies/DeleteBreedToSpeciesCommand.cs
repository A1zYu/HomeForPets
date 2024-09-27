using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.SpeciesManagement.Commands.DeleteBreedToSpecies;

public record DeleteBreedToSpeciesCommand(Guid SpeciesId,Guid BreedId) : ICommand;