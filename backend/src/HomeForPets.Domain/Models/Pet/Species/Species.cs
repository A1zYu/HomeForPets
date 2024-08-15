using CSharpFunctionalExtensions;
using HomeForPets.Domain.Models.Pet.Breed;
using HomeForPets.Domain.Models.Pet.Species;

namespace HomeForPets.Domain.Models.Pet.Species;

public class Species: Shared.Entity<SpeciesId>
{
    private readonly List<Breed.Breed> _breeds = [];
    private Species() : base(SpeciesId.NewId)
    {}
    private Species(SpeciesId speciesId,string name, List<Breed.Breed> breeds)
        : base(speciesId)
    {
        Name = name;
        AddBreeds(breeds);
    }
    
    public string Name { get; private set; }
    public IReadOnlyList<Breed.Breed> Breeds => _breeds;
    private void AddBreeds(List<Breed.Breed> breeds) => _breeds.AddRange(breeds);

    public static Result<Species> Create(SpeciesId speciesId,string name, List<Breed.Breed> breeds)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constraints.Constraints.LOW_VALUE_LENGTH)
        {
            return Result.Failure<Species>(
                $"{name} cannot be null or have length more than {Constraints.Constraints.LOW_VALUE_LENGTH}");
        }

        return Result.Success<Species>(new Species(speciesId,name, breeds ?? []));
    }
    
}