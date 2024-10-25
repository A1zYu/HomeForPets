using CSharpFunctionalExtensions;
using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Database;
using HomeForPets.Application.Dtos.Volunteers;
using HomeForPets.Application.Extensions;
using HomeForPets.Application.Model;
using HomeForPets.Domain.Shared;

namespace HomeForPets.Application.VolunteersManagement.Queries.GetVolunteersWithPagination;

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