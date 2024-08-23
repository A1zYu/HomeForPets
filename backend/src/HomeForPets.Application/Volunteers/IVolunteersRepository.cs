using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;

namespace HomeForPets.Application.Volunteers;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken ct=default);
    Task<Result<Volunteer,Error>> GetById(VolunteerId volunteerId);
    Task<Volunteer?> GetByPhoneNumber(PhoneNumber phoneNumber);
}