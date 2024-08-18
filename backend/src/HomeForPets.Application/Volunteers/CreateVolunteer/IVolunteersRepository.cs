using CSharpFunctionalExtensions;
using HomeForPets.Domain.Models.Volunteer;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken ct=default);
    Task<Result<Volunteer>> GetById(VolunteerId volunteerId);
}