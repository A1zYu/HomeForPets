using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.Core.Ids;

namespace HomeForPets.Species.Domain;

public class Breed: Core.Entity<BreedId>
{
    private Breed(BreedId id) : base(id) {}
    private Breed(BreedId breedId, string name) : base(breedId)
    {
        Name = name;
    }
    
    public string Name { get; private set; }
    
    
    public static Result<Breed,Error> Create(BreedId breedId,string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.LOW_VALUE_LENGTH)
            return Errors.General.ValueIsInvalid("Breed name");

        var breed = new Breed(breedId,name);

        return breed;
    }
}