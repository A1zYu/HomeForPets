using HomeForPets.Core.Abstactions;

namespace HomeForPets.Species.Application.SpeciesManagement.Commands.DeleteBreedToSpecies;

public record DeleteBreedToSpeciesCommand(Guid SpeciesId,Guid BreedId) : ICommand;