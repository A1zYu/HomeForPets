using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.Core.Abstaction;
using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.Core.Extensions;
using HomeForPets.Core.Model;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler
    : IQueryHandler<PagedList<VolunteerDto>, GetVolunteerWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<PagedList<VolunteerDto>,ErrorList>> Handle(GetVolunteerWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        volunteersQuery =
            volunteersQuery
                .WhereIf(query.WorkExperience != null, x => x.YearsOfExperience > query.WorkExperience);

        return await volunteersQuery.ToPagedList(query.Page, query.PageSize, cancellationToken);
    }
}