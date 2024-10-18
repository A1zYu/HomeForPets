using HomeForPets.Core.Enums;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.ChangePetStatus;

namespace HomeForPets.Volunteers.Controllers.Request;

public record ChangeStatusPetRequest(Guid PetId,
    HelpStatus HelpStatus )
{
    public PetChangeStatusCommand ToCommand(Guid id)=>new(id,PetId,HelpStatus);
}