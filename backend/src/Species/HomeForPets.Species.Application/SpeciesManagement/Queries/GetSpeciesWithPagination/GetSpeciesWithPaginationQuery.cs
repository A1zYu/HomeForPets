using HomeForPets.Core.Abstactions;

namespace HomeForPets.Species.Application.SpeciesManagement.Queries.GetSpeciesWithPagination;

public record GetSpeciesWithPaginationQuery(
    string? SortDirection,
    int Page,
    int PageSize) : IQuery
{
    
}