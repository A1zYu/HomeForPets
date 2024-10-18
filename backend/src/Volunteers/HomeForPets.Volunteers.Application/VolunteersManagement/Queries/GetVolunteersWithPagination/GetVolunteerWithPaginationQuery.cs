using HomeForPets.Core.Abstactions;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetVolunteersWithPagination;

public record GetVolunteerWithPaginationQuery(int? WorkExperience ,int Page, int PageSize) : IQuery;