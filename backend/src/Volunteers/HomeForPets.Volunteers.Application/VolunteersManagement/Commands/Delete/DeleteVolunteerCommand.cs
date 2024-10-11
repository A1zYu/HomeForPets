using HomeForPets.Core.Abstaction;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;