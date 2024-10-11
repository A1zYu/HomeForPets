using HomeForPets.Core.Abstaction;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetVolunteerById;

public class GetVolunteerByIdQuery : IQuery
{
    public Guid Id { get; set; }
}