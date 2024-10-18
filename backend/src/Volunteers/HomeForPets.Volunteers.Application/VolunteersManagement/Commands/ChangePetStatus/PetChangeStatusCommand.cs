using HomeForPets.Core.Abstactions;
using HomeForPets.Core.Enums;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.ChangePetStatus;

public record PetChangeStatusCommand(
    Guid VolunteerId,
    Guid PetId,
    HelpStatus HelpStatus
) : ICommand;