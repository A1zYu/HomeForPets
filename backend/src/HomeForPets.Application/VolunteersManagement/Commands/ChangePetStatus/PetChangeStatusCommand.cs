using HomeForPets.Application.Abstaction;
using HomeForPets.Domain.VolunteersManagement.Enums;

namespace HomeForPets.Application.VolunteersManagement.Commands.ChangePetStatus;

public record PetChangeStatusCommand(
    Guid VolunteerId,
    Guid PetId,
    HelpStatus HelpStatus
) : ICommand;