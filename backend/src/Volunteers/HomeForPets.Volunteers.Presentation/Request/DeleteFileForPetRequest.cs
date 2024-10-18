using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.DeletePhotoPet;

namespace HomeForPets.Volunteers.Controllers.Request;

public record DeleteFileForPetRequest(Guid PetId, Guid PhotoId)
{
    public DeletePhotoPetCommand ToCommand(Guid volunteerId) => new(volunteerId, PetId, PhotoId);
}