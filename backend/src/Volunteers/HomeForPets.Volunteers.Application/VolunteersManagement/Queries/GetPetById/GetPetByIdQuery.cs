using HomeForPets.Core.Abstactions;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetPetById;

public record GetPetByIdQuery(Guid PetId) : IQuery;