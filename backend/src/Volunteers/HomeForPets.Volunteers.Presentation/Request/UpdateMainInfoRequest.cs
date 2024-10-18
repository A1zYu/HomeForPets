using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.Update;

namespace HomeForPets.Volunteers.Controllers.Request;

public record UpdateMainInfoRequest(
    FullNameDto FullNameDto,
    string Description,
    int WorkExperience,
    string PhoneNumber)
{
    public UpdateMainInfoCommand ToCommand(Guid id) => new(id,FullNameDto,Description,WorkExperience,PhoneNumber);
}