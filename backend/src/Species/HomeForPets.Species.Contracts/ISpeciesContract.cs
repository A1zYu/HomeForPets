using CSharpFunctionalExtensions;
using HomeForPets.Core.Dtos.SpeciesDto;
using HomeForPets.SharedKernel;

namespace HomeForPets.Species.Contacts.Interfaces;

public interface ISpeciesContract
{
    public Task<Result<SpeciesDto, Error>> GetSpeciesById(
        Guid speciesId, CancellationToken cancellationToken);
}