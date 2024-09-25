using HomeForPets.Application.VolunteersManagement.Queries;

namespace HomeForPets.Api.Controllers.Volunteers.Request;

public record GetVolunteersWithPaginationRequest(int? WorkExperience, int Page, int PageSize)
{
    public GetVolunteerWithPaginationQuery ToQuery()=> new(WorkExperience,Page, PageSize);
}