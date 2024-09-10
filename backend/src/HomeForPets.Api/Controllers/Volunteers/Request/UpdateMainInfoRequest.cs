using HomeForPets.Application.Dtos;
using HomeForPets.Application.Volunteers.Update;

namespace HomeForPets.Api.Controllers.Volunteers.Request;

public record UpdateMainInfoRequest(
    FullNameDto FullNameDto,
    string Description,
    int WorkExperience,
    string PhoneNumber)
{
    public UpdateMainInfoCommand ToCommand(Guid id) => new(id,FullNameDto,Description,WorkExperience,PhoneNumber);
}