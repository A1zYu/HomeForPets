using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.VolunteersManagement.Queries.GetVolunteerById;

public class GetVolunteerByIdQuery : IQuery
{
    public Guid Id { get; set; }
}