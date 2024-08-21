using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;

namespace HomeForPets.Domain.Species;

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

    public static Result<Species,Error> Create(SpeciesId speciesId,string name, List<Breed> breeds)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constraints.Constraints.LOW_VALUE_LENGTH)
        {
            return Errors.General.Validation("Species name");
        }
        
        return new Species(speciesId,name,breeds);
    }
    
}