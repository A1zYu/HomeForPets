using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.SpeciesManagement.Queries.GetSpeciesWithPagination;

public record GetSpeciesWithPaginationQuery(
    string? SortDirection,
    int Page,
    int PageSize) : IQuery
{
    
}