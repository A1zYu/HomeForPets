using HomeForPets.Application.VolunteersManagement.Commands.DeletePhotoPet;

namespace HomeForPets.Api.Controllers.Volunteers.Request;

public record DeleteFileForPetRequest(Guid PetId, Guid PhotoId)
{
    public DeletePhotoPetCommand ToCommand(Guid volunteerId) => new(volunteerId, PetId, PhotoId);
}