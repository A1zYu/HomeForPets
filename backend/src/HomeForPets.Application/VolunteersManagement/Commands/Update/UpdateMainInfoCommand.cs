using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.VolunteersManagement.Commands.Update;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullNameDto,
    string Description,
    int WorkExperience,
    string PhoneNumber) : ICommand;