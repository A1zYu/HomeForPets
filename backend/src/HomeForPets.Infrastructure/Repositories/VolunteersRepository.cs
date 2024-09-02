using CSharpFunctionalExtensions;
using HomeForPets.Application.Volunteers;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;
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

    public async Task<Result<Volunteer,Error>> GetById(
        VolunteerId id, 
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound();

        return volunteer;
    }

    public async Task<Volunteer?> GetByPhoneNumber(PhoneNumber phoneNumber)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(x =>   x.PhoneNumber == phoneNumber);
        return volunteer;
    }

    public async Task<Guid> Delete(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Remove(volunteer);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return volunteer.Id;
    }

    public async Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _dbContext.Volunteers.Attach(volunteer);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id.Value;
    }
}