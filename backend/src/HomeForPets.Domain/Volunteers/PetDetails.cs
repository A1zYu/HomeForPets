using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Domain.Volunteers;

public record PetDetails
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
    public DateTime BirthOfDate { get;}
    

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
            return Errors.General.Validation("Color");
        }

        if (string.IsNullOrWhiteSpace(healthInfo))
        {
            return Errors.General.Validation("Health info");
        }

        if (weight <= 0)
        {
            return Errors.General.Validation("Weight");
        }

        if (height <= 0)
        {
            return Errors.General.Validation("Height");
        }
        if (birthOfDate.Date > DateTime.Now.Date)
        {
            return Errors.General.Validation("Birth Of Date");
        }

        var details = new PetDetails(color, healthInfo, weight, height, isVaccinated, isNeutered,birthOfDate);
        return details;
    }
    
}