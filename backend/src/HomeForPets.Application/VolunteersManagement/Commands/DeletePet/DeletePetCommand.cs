using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.VolunteersManagement.Commands.DeletePet;

public record DeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;