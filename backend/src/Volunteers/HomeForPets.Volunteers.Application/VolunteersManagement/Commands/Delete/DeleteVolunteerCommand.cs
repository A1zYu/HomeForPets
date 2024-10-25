using HomeForPets.Core.Abstactions;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.Delete;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;