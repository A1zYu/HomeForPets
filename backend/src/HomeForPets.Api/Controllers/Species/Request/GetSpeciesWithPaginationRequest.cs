using HomeForPets.Application.SpeciesManagement.Queries.GetSpeciesWithPagination;

namespace HomeForPets.Api.Controllers.Species.Request;

public record GetSpeciesWithPaginationRequest(
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetSpeciesWithPaginationQuery ToQuery() =>
        new(SortDirection, Page, PageSize);
}