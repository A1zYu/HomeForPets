using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.SharedKernel;
using HomeForPets.SharedKernel.Ids;
using HomeForPets.Species.Domain;

namespace HomeForPets.Species.Application.SpeciesManagement;

public interface ISpeciesRepository
{
    Task<Guid> Add(Domain.Species species, CancellationToken ct=default);
    Task<Result<Domain.Species,Error>> GetById(SpeciesId speciesId, CancellationToken ct=default);
    Task<UnitResult<Error>> Delete(Domain.Species species, CancellationToken ct);
    
}