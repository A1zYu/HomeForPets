using HomeForPets.Core.Abstaction;

namespace HomeForPets.Species.Application.SpeciesManagement.Queries.GetBreedsBySpecial;

public record GetBreedsBySpecialQuery(Guid SpecialId) : IQuery;