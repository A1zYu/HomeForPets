using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Species;

namespace HomeForPets.Application.SpeciesManagement;

public interface ISpeciesRepository
{
    Task<Guid> Add(Species species, CancellationToken ct=default);
    Task<Result<Species,Error>> GetById(SpeciesId speciesId, CancellationToken ct=default);
    Task<UnitResult<Error>> Delete(Species species, CancellationToken ct);
}