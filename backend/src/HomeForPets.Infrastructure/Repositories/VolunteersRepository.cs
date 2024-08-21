using CSharpFunctionalExtensions;
using HomeForPets.Application.Volunteers;
using HomeForPets.Application.Volunteers.CreateVolunteer;
using HomeForPets.Domain.Models.Volunteer;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.ValueObjects;
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

    public async Task<Result<Volunteer,Error>> GetById(VolunteerId volunteerId)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(x=>x.PaymentDetailsList)
            .Include(x=>x.Pets)
            .ThenInclude(x=>x.PetPhotos)
            .FirstOrDefaultAsync(v => v.Id == volunteerId);
        if (volunteer is null)
        {
            return Errors.General.NotFound();
        }

        return volunteer;
    }

    public async Task<Volunteer?> GetByPhoneNumber(PhoneNumber phoneNumber)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(x => x.Contact != null && x.Contact.PhoneNumber == phoneNumber);
        return volunteer;
    }
}