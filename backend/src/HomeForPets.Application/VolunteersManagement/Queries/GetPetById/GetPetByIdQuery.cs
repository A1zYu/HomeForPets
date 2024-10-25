using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.VolunteersManagement.Queries.GetPetById;

public record GetPetByIdQuery(Guid PetId) : IQuery;