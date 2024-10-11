using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.Core.Abstaction;
using HomeForPets.Core.Dtos.SpeciesDto;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Species.Application.SpeciesManagement.Queries.GetBreedsBySpecial;

public class GetBreedsBySpecialHandler : IQueryHandler<List<BreedDto>, GetBreedsBySpecialQuery>
{
    private readonly ILogger<GetBreedsBySpecialHandler> _logger;
    // private readonly IReadDbContext _readDbContext;

    public GetBreedsBySpecialHandler(
        ILogger<GetBreedsBySpecialHandler> logger
        // IReadDbContext readDbContext
        )
    {
        _logger = logger;
        // _readDbContext = readDbContext;
    }

    public async Task<Result<List<BreedDto>,ErrorList>> Handle(GetBreedsBySpecialQuery query, CancellationToken ct)
    {
        // var breedsQuery = _readDbContext.Breeds;
        
        // breedsQuery = breedsQuery.Where(b => b.SpeciesId == query.SpecialId);

        // return breedsQuery.ToList();
        return new Result<List<BreedDto>, ErrorList>();
    }
}