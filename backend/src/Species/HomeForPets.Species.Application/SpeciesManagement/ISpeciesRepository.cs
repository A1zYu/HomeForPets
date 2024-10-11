using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.Core.Ids;
using HomeForPets.Species.Domain;

namespace HomeForPets.Species.Application.SpeciesManagement;

public interface ISpeciesRepository
{
    Task<Guid> Add(Specie species, CancellationToken ct=default);
    Task<Result<Specie,Error>> GetById(SpeciesId speciesId, CancellationToken ct=default);
    Task<UnitResult<Error>> Delete(Specie species, CancellationToken ct);
    
}