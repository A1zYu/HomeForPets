using HomeForPets.Core.Abstactions;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.DeletePet;

public record DeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;