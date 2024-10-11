using HomeForPets.Core.Abstaction;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetPetById;

public record GetPetByIdQuery(Guid PetId) : IQuery;