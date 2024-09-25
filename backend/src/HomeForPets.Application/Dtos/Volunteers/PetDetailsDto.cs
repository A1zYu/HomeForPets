namespace HomeForPets.Application.Dtos.Volunteers;

public record PetDetailsDto(
    string Color,
    string HealthInfo,
    double Weight,
    double Height,
    bool IsVaccinated,
    bool IsNeutered,
    DateTime BirthOfDate);