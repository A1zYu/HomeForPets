using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.VolunteersManagement.Queries.GetVolunteerById;

public class GetVolunteerByIdCommand : ICommand
{
    public Guid Id { get; set; }
}