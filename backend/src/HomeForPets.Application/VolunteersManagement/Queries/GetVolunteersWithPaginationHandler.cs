using Dapper;
using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Database;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.Volunteers;
using HomeForPets.Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HomeForPets.Application.VolunteersManagement.Queries;

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

public class GetVolunteersWithPaginationHandlerDapper
    : IQueryHandler<PagedList<VolunteerDto>, GetVolunteerWithPaginationQuery>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetVolunteersWithPaginationHandlerDapper(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<PagedList<VolunteerDto>> Handle(GetVolunteerWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.CreateConnection();

        var parameters = new DynamicParameters();

        var totalCount = await connection.ExecuteScalarAsync<long>("select count(*) from volunteers");

        var sql = """
                  Select first_name,last_name, payment_details from volunteers 
                  LIMIT @PageSize offset @Offset
                  """;

        parameters.Add("@PageSize", query.PageSize);
        parameters.Add("@Offset", (query.Page - 1) * query.PageSize);
        
        var volunteers = await connection.QueryAsync<VolunteerDto>(sql, parameters);
        return new PagedList<VolunteerDto>()
        {
            Items = volunteers.ToList(),
            TotalCount = totalCount,
            PageSize = query.PageSize,
            Page = query.Page,
        };
    }
}