using HomeForPets.Core.Abstaction;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetVolunteersWithPagination;

public record GetVolunteerWithPaginationQuery(int? WorkExperience ,int Page, int PageSize) : IQuery;