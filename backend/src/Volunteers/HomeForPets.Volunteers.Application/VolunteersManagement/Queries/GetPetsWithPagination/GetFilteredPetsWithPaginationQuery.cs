using HomeForPets.Core.Abstactions;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetPetsWithPagination;

// Обязательно должна быть пагинация и фильтрация. Нужно фильтровать питомцев 
// по id волонтёров, имени, возрасту, породе, окрасу, виду, возрасту, месту проживания и другим свойствам.
public record GetFilteredPetsWithPaginationQuery(
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
    string? SortDirection
) : IQuery;