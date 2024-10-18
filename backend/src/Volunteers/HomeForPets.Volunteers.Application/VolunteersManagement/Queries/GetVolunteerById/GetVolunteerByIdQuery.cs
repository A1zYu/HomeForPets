using HomeForPets.Core.Abstactions;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetVolunteerById;

public class GetVolunteerByIdQuery : IQuery
{
    public Guid Id { get; set; }
}