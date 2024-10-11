using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.Core.Abstaction;
using HomeForPets.Core.Dtos.SpeciesDto;
using HomeForPets.Core.Model;

namespace HomeForPets.Species.Application.SpeciesManagement.Queries.GetSpeciesWithPagination;

public class GetSpeciesWithPaginationHandler : IQueryHandler<PagedList<SpeciesDto>,GetSpeciesWithPaginationQuery>
{
    // private readonly IReadDbContext _readDbContext;

    public GetSpeciesWithPaginationHandler(
        // IReadDbContext readDbContext
        )
    {
        // _readDbContext = readDbContext;
    }

    public async Task<Result<PagedList<SpeciesDto>, ErrorList>> Handle(GetSpeciesWithPaginationQuery query, CancellationToken ct)
    {
        // var speciesQuery = _readDbContext.Species;

        // speciesQuery = speciesQuery.Include(x=>x.BreedDtos);
        
        // speciesQuery = query.SortDirection?.ToLower() == "desc"
            // ? speciesQuery.OrderByDescending(keySelector => keySelector.Name)
            // : speciesQuery.OrderBy(keySelector => keySelector.Name);
        
        // return await speciesQuery.ToPagedList(query.Page, query.PageSize, ct);
        return new Result<PagedList<SpeciesDto>, ErrorList>();
    }
}