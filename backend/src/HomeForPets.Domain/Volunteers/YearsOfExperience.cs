using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Domain.Volunteers;

public record YearsOfExperience
{
    private YearsOfExperience()
    {
        
    }
    private YearsOfExperience(int? value)
    {
        Value = value;
    }
    public int? Value { get; }

    public static Result<YearsOfExperience, Error> Create(int value)
    {
        if (value is < 0 or > 80)
        {
            return Errors.General.ValueIsInvalid("Years of experience");
        }
        return new YearsOfExperience(value);
    }
}