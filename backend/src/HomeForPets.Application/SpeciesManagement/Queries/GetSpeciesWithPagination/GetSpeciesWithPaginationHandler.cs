using CSharpFunctionalExtensions;
using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Database;
using HomeForPets.Application.Dtos.SpeciesDto;
using HomeForPets.Application.Extensions;
using HomeForPets.Application.Model;
using HomeForPets.Application.VolunteersManagement;
using HomeForPets.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.SpeciesManagement.Queries.GetSpeciesWithPagination;

public class GetSpeciesWithPaginationHandler : IQueryHandler<PagedList<SpeciesDto>,GetSpeciesWithPaginationQuery>
{
    private readonly ILogger<GetSpeciesWithPaginationHandler> _logger;
    private readonly IReadDbContext _readDbContext;

    public GetSpeciesWithPaginationHandler(ILogger<GetSpeciesWithPaginationHandler> logger, IReadDbContext readDbContext)
    {
        _logger = logger;
        _readDbContext = readDbContext;
    }

    public async Task<Result<PagedList<SpeciesDto>, ErrorList>> Handle(GetSpeciesWithPaginationQuery query, CancellationToken ct)
    {
        var speciesQuery = _readDbContext.Species;

        speciesQuery = speciesQuery.Include(x=>x.BreedDtos);
        
        speciesQuery = query.SortDirection?.ToLower() == "desc"
            ? speciesQuery.OrderByDescending(keySelector => keySelector.Name)
            : speciesQuery.OrderBy(keySelector => keySelector.Name);
        
        return await speciesQuery.ToPagedList(query.Page, query.PageSize, ct);
    }
}