using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.Core.Ids;
using HomeForPets.Volunteers.Application.VolunteersManagement;
using HomeForPets.Volunteers.Domain;
using HomeForPets.Volunteers.Domain.ValueObjects;
using HomeForPets.Volunteers.Infrastucture.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HomeForPets.Volunteers.Infrastucture.Repositories;

public class VolunteersRepository : IVolunteersRepository
{
    private readonly WriteDbContext _dbContext;

    public VolunteersRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken ct = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, ct);
        return volunteer.Id;
    }

    public Guid Delete(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Remove(volunteer);
        return volunteer.Id;
    }

    public Guid Save(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _dbContext.Volunteers.Attach(volunteer);
        return volunteer.Id.Value;
    }

    public async Task<Result<Volunteer, Error>> GetById(
        VolunteerId id,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (volunteer is null)
            return Errors.General.NotFound(id.Value);

        return volunteer;
    }

    public async Task<Volunteer?> GetByPhoneNumber(PhoneNumber phoneNumber)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        return volunteer;
    }
}