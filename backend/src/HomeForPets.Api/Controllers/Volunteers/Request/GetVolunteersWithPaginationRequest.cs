using HomeForPets.Application.VolunteersManagement.Queries;
using HomeForPets.Application.VolunteersManagement.Queries.GetVolunteersWithPagination;

namespace HomeForPets.Api.Controllers.Volunteers.Request;

public record GetVolunteersWithPaginationRequest(int? WorkExperience, int Page, int PageSize)
{
    public GetVolunteerWithPaginationQuery ToQuery()=> new(WorkExperience,Page, PageSize);
}