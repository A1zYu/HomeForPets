using HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetPetsWithPagination;

namespace HomeForPets.Volunteers.Controllers.Request;

public record GetPetsWithPaginationRequest(
    int PageNumber,
    int PageSize,
    Guid? VolunteerId,
    string? Color,
    Guid? BreedId,
    Guid? SpeciesId,
    string? Name,
    string? City,
    int? PositionFrom,
    int? PositionTo,
    string? SortBy,
    string? SortDirection)
{
    
    public GetFilteredPetsWithPaginationQuery ToQuery()=>new(
        PageNumber,
        PageSize,
        VolunteerId,
        Color,
        BreedId,
        SpeciesId,
        Name,
        City,
        PositionFrom,
        PositionTo,
        SortBy,
        SortDirection);
}