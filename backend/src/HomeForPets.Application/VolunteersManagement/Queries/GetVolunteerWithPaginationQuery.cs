using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.VolunteersManagement.Queries;

public record GetVolunteerWithPaginationQuery(int? WorkExperience ,int Page, int PageSize) : IQuery;