using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Volunteers;

namespace HomeForPets.Application.Volunteers;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken ct=default);
    Task<Result<Volunteer,Error>> GetById(VolunteerId volunteerId, CancellationToken ct=default);
    Task<Volunteer?> GetByPhoneNumber(PhoneNumber phoneNumber);
    Task<Guid> Delete(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken);
}