using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.VolunteersManagement.Commands.DeletePhotoPet;

public record DeletePhotoPetCommand(Guid VolunteerId, Guid PetId,Guid PhotoId) : ICommand;