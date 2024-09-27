using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Database;
using HomeForPets.Application.Dtos.Volunteers;
using HomeForPets.Application.Extensions;

namespace HomeForPets.Application.VolunteersManagement.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler
    : IQueryHandler<PagedList<VolunteerDto>, GetVolunteerWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<VolunteerDto>> Handle(GetVolunteerWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        volunteersQuery =
            volunteersQuery
                .WhereIf(query.WorkExperience != null, x => x.YearsOfExperience > query.WorkExperience);

        return await volunteersQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}