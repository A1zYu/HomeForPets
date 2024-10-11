using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.Core.Abstaction;
using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.Core.Extensions;
using HomeForPets.Core.Model;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetPetsWithPagination;

public class GetFilteredPetsWithPaginationHandler : IQueryHandler<PagedList<PetDto>, GetFilteredPetsWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetFilteredPetsWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<PagedList<PetDto>, ErrorList>> Handle(GetFilteredPetsWithPaginationQuery query,
        CancellationToken ct)
    {
        var petsQuery = _readDbContext.Pets;

        Expression<Func<PetDto, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "name" => (pet) => pet.Name,
            "position" => (pet) => pet.Position,
            "city" => (pet) => pet.City,
            // "breed" => (pet) => pet.BreedName,
            // "species"=>(pet)=>pet.SpeciesName,
            _ => (issue) => issue.Id
        };
        petsQuery = query.SortDirection?.ToLower() == "desc"
            ? petsQuery.OrderByDescending(keySelector)
            : petsQuery.OrderBy(keySelector);
        
        petsQuery = petsQuery
            .WhereIf(!string.IsNullOrWhiteSpace(query.Name), x => x.Name == query.Name);

        petsQuery = petsQuery
            .WhereIf(!string.IsNullOrWhiteSpace(query.Color), x => x.Color == query.Color);

        petsQuery = petsQuery
            .WhereIf(!string.IsNullOrWhiteSpace(query.City), x => x.City == query.City);
        
        petsQuery = petsQuery.WhereIf(
            query.PositionTo != null,
            i => i.Position <= query.PositionTo!.Value);

        petsQuery = petsQuery.WhereIf(
            query.PositionFrom != null,
            i => i.Position >= query.PositionFrom!.Value);
        
        petsQuery = petsQuery
            .OrderByDescending(p => p.PetPhotos.Any(photo => photo.IsMain));
        
        return await petsQuery.ToPagedList(query.PageNumber, query.PageSize, ct);
    }
}