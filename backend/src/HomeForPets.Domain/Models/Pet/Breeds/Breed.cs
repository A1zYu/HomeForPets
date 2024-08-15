using CSharpFunctionalExtensions;

namespace HomeForPets.Domain.Models.Pet.Breeds;

public class Breed: Shared.Entity<BreedId>
{
    private Breed() : base(BreedId.NewId())
    {}
    private Breed(BreedId breedId, string name) : base(breedId)
    {
        Name = name;
    }
    
    public string Name { get; private set; }
    
    
    public static Result<Breed> Create(BreedId breedId,string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constraints.Constraints.LOW_VALUE_LENGTH)
            return Result.Failure<Breed>($"{nameof(name)} cannot be null or length more than {Constraints.Constraints.LOW_VALUE_LENGTH}");

        var breed = new Breed(breedId,name);

        return Result.Success(breed);
    }
}