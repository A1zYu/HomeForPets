using CSharpFunctionalExtensions;
using HomeForPets.Core.Dtos.SpeciesDto;
using HomeForPets.SharedKernel;
using HomeForPets.Species.Application;
using HomeForPets.Species.Contacts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeForPets.Species.Controllers;

public class SpeciesContract(ISpeciesReadDbContext readDbContext) : ISpeciesContract
{
    public async Task<Result<SpeciesDto, Error>> GetSpeciesById(Guid speciesId, CancellationToken cancellationToken)
    {
        var result = await readDbContext.Species.Include(s => s.BreedDtos)
            .FirstOrDefaultAsync(s => s.Id == speciesId, cancellationToken);

        if (result is not null)
            return result;

        return Errors.General.NotFound(speciesId);
    }
}