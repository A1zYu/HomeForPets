using CSharpFunctionalExtensions;
using HomeForPets.SharedKernel;
using HomeForPets.SharedKernel.Ids;
using HomeForPets.Species.Application;
using HomeForPets.Species.Application.SpeciesManagement;
using HomeForPets.Species.Domain;
using HomeForPets.Species.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HomeForPets.Species.Infrastructure;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly SpeciesWriteDbContext _writeDbContext;

    public SpeciesRepository(SpeciesWriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task<Guid> Add(Domain.Species species, CancellationToken ct = default)
    {
        await _writeDbContext.Species.AddAsync(species, ct);
        return species.Id;
    }

    public async Task<Result<Domain.Species, Error>> GetById(SpeciesId speciesId, CancellationToken ct = default)
    {
        var species = await _writeDbContext.Species.Include(x => x.Breeds)
            .FirstOrDefaultAsync(species => species.Id == speciesId, ct);
        if (species is null)
        {
            return Errors.General.NotFound(speciesId);
        }

        return species;
    }

    public async Task<UnitResult<Error>> Delete(Domain.Species species, CancellationToken ct)
    {
        _writeDbContext.Species.Remove(species);

        return UnitResult.Success<Error>();
    }
    
}