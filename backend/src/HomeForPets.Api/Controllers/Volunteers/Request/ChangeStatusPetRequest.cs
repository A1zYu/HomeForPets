using HomeForPets.Application.VolunteersManagement.Commands.ChangePetStatus;
using HomeForPets.Domain.VolunteersManagement.Enums;

namespace HomeForPets.Api.Controllers.Volunteers.Request;

public record ChangeStatusPetRequest(Guid PetId,
    HelpStatus HelpStatus )
{
    public PetChangeStatusCommand ToCommand(Guid id)=>new(id,PetId,HelpStatus);
}