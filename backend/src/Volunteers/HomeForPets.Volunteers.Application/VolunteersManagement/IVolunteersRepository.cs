using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.Core.Ids;
using HomeForPets.Volunteers.Domain;
using HomeForPets.Volunteers.Domain.ValueObjects;

namespace HomeForPets.Volunteers.Application.VolunteersManagement;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken ct=default);
    Task<Result<Volunteer,Error>> GetById(VolunteerId volunteerId, CancellationToken ct=default);
    Task<Volunteer?> GetByPhoneNumber(PhoneNumber phoneNumber);
    Guid Delete(Volunteer volunteer, CancellationToken cancellationToken = default);
    Guid Save(Volunteer volunteer, CancellationToken cancellationToken);
    // Task<IReadOnlyList<Volunteer>> GetWithPagination(int page,int pageSize,CancellationToken cancellationToken);
}