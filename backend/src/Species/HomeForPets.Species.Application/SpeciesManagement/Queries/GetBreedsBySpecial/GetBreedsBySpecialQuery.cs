using HomeForPets.Core.Abstactions;

namespace HomeForPets.Species.Application.SpeciesManagement.Queries.GetBreedsBySpecial;

public record GetBreedsBySpecialQuery(Guid SpecialId) : IQuery;