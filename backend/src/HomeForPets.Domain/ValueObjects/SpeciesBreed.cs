using CSharpFunctionalExtensions;
using HomeForPets.Domain.Models.Pet;
using HomeForPets.Domain.Models.Pet.Breeds;

namespace HomeForPets.Domain.ValueObjects;

public record SpeciesBreed
{
    private SpeciesBreed(SpeciesId speciesId, BreedId breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    public SpeciesId SpeciesId { get; }
    public BreedId BreedId { get; }

    public static Result<SpeciesBreed> Create(SpeciesId speciesId,BreedId breedId)
    {
        var speciesBreed = new SpeciesBreed(speciesId, breedId);
        return Result.Success(speciesBreed);
    }
}