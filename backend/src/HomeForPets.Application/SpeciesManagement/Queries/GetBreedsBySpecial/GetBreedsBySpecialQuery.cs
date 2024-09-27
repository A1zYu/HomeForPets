using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.SpeciesManagement.Queries.GetBreedsBySpecial;

public record GetBreedsBySpecialQuery(Guid SpecialId) : IQuery;