using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.Volunteers;
using HomeForPets.Application.VolunteersManagement.Commands.Update;

namespace HomeForPets.Api.Controllers.Volunteers.Request;

public record UpdateMainInfoRequest(
    FullNameDto FullNameDto,
    string Description,
    int WorkExperience,
    string PhoneNumber)
{
    public UpdateMainInfoCommand ToCommand(Guid id) => new(id,FullNameDto,Description,WorkExperience,PhoneNumber);
}