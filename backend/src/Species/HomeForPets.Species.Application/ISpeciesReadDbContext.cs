using HomeForPets.Core.Dtos.SpeciesDto;
using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.Species.Domain;

namespace HomeForPets.Species.Application;

public interface ISpeciesReadDbContext
{
    IQueryable<BreedDto> Breeds { get; }
    IQueryable<SpeciesDto> Species { get; }
    
}