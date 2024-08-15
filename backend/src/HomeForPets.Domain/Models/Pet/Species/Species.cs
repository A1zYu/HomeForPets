using CSharpFunctionalExtensions;
using HomeForPets.Domain.Models.Pet.Breeds;

namespace HomeForPets.Domain.Models.Pet;

public class Species: Shared.Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];
    private Species(SpeciesId id) : base(id) {}
    private Species(SpeciesId speciesId,string name, List<Breed> breeds)
        : base(speciesId)
    {
        Name = name;
        AddBreeds(breeds);
    }
    
    public string Name { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;
    private void AddBreeds(List<Breed> breeds) => _breeds.AddRange(breeds);

    public static Result<Species> Create(SpeciesId speciesId,string name, List<Breed> breeds)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constraints.Constraints.LOW_VALUE_LENGTH)
        {
            return Result.Failure<Species>(
                $"{name} cannot be null or have length more than {Constraints.Constraints.LOW_VALUE_LENGTH}");
        }

        return Result.Success<Species>(new Species(speciesId,name, breeds ?? []));
    }
    
}