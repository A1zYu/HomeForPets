using CSharpFunctionalExtensions;
using HomeForPets.Domain.Models.Volunteer;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.ValueObjects;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken ct=default);
    Task<Result<Volunteer,Error>> GetById(VolunteerId volunteerId);
    Task<Volunteer?> GetByPhoneNumber(PhoneNumber phoneNumber);
}