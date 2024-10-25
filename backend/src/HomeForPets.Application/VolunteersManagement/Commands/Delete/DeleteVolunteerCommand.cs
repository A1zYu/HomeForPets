using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.VolunteersManagement.Commands.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;