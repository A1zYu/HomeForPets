using HomeForPets.Core.Dtos.Volunteers;

namespace HomeForPets.Volunteers.Application;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<PetDto> Pets { get; }
    
}