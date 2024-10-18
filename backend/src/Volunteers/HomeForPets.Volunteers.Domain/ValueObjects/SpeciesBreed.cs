using CSharpFunctionalExtensions;
using HomeForPets.SharedKernel.Ids;

namespace HomeForPets.Volunteers.Domain.ValueObjects;

public record SpeciesBreed
{
    private SpeciesBreed(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    public SpeciesId SpeciesId { get; }
    public Guid BreedId { get; }

    public static Result<SpeciesBreed> Create(SpeciesId speciesId,Guid breedId)
    {
        var speciesBreed = new SpeciesBreed(speciesId, breedId);
        return Result.Success(speciesBreed);
    }
}