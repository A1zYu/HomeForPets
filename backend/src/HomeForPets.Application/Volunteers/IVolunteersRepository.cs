using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.VolunteersManagement;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;

namespace HomeForPets.Application.Volunteers;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken ct=default);
    Task<Result<Volunteer,Error>> GetById(VolunteerId volunteerId, CancellationToken ct=default);
    Task<Volunteer?> GetByPhoneNumber(PhoneNumber phoneNumber);
    Guid Delete(Volunteer volunteer, CancellationToken cancellationToken = default);
    Guid Save(Volunteer volunteer, CancellationToken cancellationToken);
    Task<List<Volunteer>> GetAll(int page,int pageSize,CancellationToken cancellationToken);
}