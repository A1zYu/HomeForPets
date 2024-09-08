using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.Volunteers.Update;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullNameDto,
    string Description,
    int WorkExperience,
    string PhoneNumber);