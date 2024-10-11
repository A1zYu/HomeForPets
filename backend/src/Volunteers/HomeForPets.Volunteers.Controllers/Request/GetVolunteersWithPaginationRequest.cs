using HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetVolunteersWithPagination;

namespace HomeForPets.Volunteers.Controllers.Request;

public record GetVolunteersWithPaginationRequest(int? WorkExperience, int Page, int PageSize)
{
    public GetVolunteerWithPaginationQuery ToQuery()=> new(WorkExperience,Page, PageSize);
}