using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.Volunteers;

namespace HomeForPets.Application.VolunteersManagement.Commands.Update;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullNameDto,
    string Description,
    int WorkExperience,
    string PhoneNumber) : ICommand;