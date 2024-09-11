using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Domain.Volunteers;

public class PetDetails : ValueObject
{
    public PetDetails()
    {
    }

    private PetDetails(
        string color,
        string healthInfo,
        double weight,
        double height,
        bool isVaccinated,
        bool isNeutered,
        DateTime birthOfDate)
    {
        Color = color;
        HealthInfo = healthInfo;
        Weight = weight;
        Height = height;
        IsVaccinated = isVaccinated;
        IsNeutered = isNeutered;
        BirthOfDate = birthOfDate;
    }

    public string Color { get; }
    public string HealthInfo { get; }
    public double Weight { get; }
    public double Height { get; }
    public bool IsVaccinated { get; }
    public bool IsNeutered { get; }
    public DateTime BirthOfDate { get; }


    public static Result<PetDetails, Error> Create(
        string color,
        string healthInfo,
        double weight,
        double height,
        bool isVaccinated,
        bool isNeutered,
        DateTime birthOfDate)
    {
        if (string.IsNullOrWhiteSpace(color))
        {
            return Errors.General.ValueIsInvalid("Color");
        }

        if (string.IsNullOrWhiteSpace(healthInfo))
        {
            return Errors.General.ValueIsInvalid("Health info");
        }

        if (weight <= 0)
        {
            return Errors.General.ValueIsInvalid("Weight");
        }

        if (height <= 0)
        {
            return Errors.General.ValueIsInvalid("Height");
        }

        if (birthOfDate.Date > DateTime.Now.Date)
        {
            return Errors.General.ValueIsInvalid("Birth Of Date");
        }

        var details = new PetDetails(color, healthInfo, weight, height, isVaccinated, isNeutered, birthOfDate);
        return details;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Color;
        yield return HealthInfo;
        yield return Weight;
        yield return Height;
        yield return IsVaccinated;
        yield return IsNeutered;
        yield return BirthOfDate;
    }
}