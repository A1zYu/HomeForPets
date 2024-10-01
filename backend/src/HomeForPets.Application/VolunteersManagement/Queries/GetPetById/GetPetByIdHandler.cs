using CSharpFunctionalExtensions;
using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Database;
using HomeForPets.Application.Dtos.Volunteers;
using HomeForPets.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace HomeForPets.Application.VolunteersManagement.Queries.GetPetById;

public class GetPetByIdHandler : IQueryHandler<PetDto,GetPetByIdQuery>
{
    private readonly IReadDbContext _readDbContext;
    public GetPetByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<Result<PetDto, ErrorList>> Handle(GetPetByIdQuery query, CancellationToken ct)
    {
        var petQuery = _readDbContext.Pets
            .Include(x=>x.PetPhotos)
            .FirstOrDefault(x=>x.Id == query.PetId);
        if (petQuery is null)
        {
            return Errors.General.NotFound(query.PetId).ToErrorList();
        }

        return petQuery;
    }
}