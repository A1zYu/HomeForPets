using HomeForPets.Core.Abstactions;
using HomeForPets.Core.Dtos.Volunteers;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.Update;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullNameDto,
    string Description,
    int WorkExperience,
    string PhoneNumber) : ICommand;