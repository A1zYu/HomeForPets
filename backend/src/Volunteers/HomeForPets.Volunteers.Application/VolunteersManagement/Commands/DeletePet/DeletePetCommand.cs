using HomeForPets.Core.Abstaction;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.DeletePet;

public record DeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;