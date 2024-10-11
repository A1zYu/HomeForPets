using HomeForPets.Core.Abstaction;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.DeletePhotoPet;

public record DeletePhotoPetCommand(Guid VolunteerId, Guid PetId,Guid PhotoId) : ICommand;