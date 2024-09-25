using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.Volunteers;
using Microsoft.EntityFrameworkCore;

namespace HomeForPets.Application.Database;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<PetDto> Pets { get; }
}