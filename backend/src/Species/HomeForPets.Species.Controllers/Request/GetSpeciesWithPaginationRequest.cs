using HomeForPets.Species.Application.SpeciesManagement.Queries.GetSpeciesWithPagination;

namespace HomeForPets.Species.Controllers.Request;

public record GetSpeciesWithPaginationRequest(
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetSpeciesWithPaginationQuery ToQuery() =>
        new(SortDirection, Page, PageSize);
}