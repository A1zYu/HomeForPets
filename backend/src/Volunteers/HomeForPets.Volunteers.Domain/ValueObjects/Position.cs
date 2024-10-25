using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.SharedKernel;

namespace HomeForPets.Volunteers.Domain.ValueObjects;

public class Position : ValueObject
{
    public static Position First => new(1);
    public int Value { get; }
    private Position(int value)
    {
        Value = value;
    }
    public Result<Position, Error> Forward() =>
        Create(Value + 1);

    public Result<Position, Error> Back() =>
        Create(Value - 1);
    public static Result<Position, Error> Create(int value)
    {
        if (value < 1)
        {
            return Errors.General.ValueIsInvalid("position number");
        }

        return new Position(value);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}