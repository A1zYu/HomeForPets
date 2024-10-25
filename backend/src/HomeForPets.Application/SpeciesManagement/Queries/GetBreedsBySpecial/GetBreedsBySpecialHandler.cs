using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Database;
using HomeForPets.Application.Dtos.SpeciesDto;
using HomeForPets.Application.Extensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Species;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.SpeciesManagement.Queries.GetBreedsBySpecial;

public class GetBreedsBySpecialHandler : IQueryHandler<List<BreedDto>, GetBreedsBySpecialQuery>
{
    private readonly ILogger<GetBreedsBySpecialHandler> _logger;
    private readonly IReadDbContext _readDbContext;

    public GetBreedsBySpecialHandler(
        ILogger<GetBreedsBySpecialHandler> logger,
        IReadDbContext readDbContext)
    {
        _logger = logger;
        _readDbContext = readDbContext;
    }

    public async Task<Result<List<BreedDto>,ErrorList>> Handle(GetBreedsBySpecialQuery query, CancellationToken ct)
    {
        var breedsQuery = _readDbContext.Breeds;
        
        breedsQuery = breedsQuery.Where(b => b.SpeciesId == query.SpecialId);

        return breedsQuery.ToList();
    }
}