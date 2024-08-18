using CSharpFunctionalExtensions;
using HomeForPets.Application.Volunteers.CreateVolunteer;
using HomeForPets.Domain.Models.Volunteer;
using Microsoft.EntityFrameworkCore;

namespace HomeForPets.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly ApplicationDbContext _dbContext;
    public VolunteersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken ct=default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer,ct);
        await _dbContext.SaveChangesAsync(ct);
        return volunteer.Id;
    }

    public async Task<Result<Volunteer>> GetById(VolunteerId volunteerId)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(x=>x.PaymentDetailsList)
            .Include(x=>x.Pets)
            .ThenInclude(x=>x.PetPhotos)
            .FirstOrDefaultAsync(v => v.Id == volunteerId);
        if (volunteer is null)
        {
            return Result.Failure<Volunteer>("Volunteer not found");
        }

        return volunteer;
    }
}