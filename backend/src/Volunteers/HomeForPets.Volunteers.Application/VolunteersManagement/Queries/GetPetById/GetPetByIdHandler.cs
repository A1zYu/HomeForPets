using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.Core.Abstactions;
using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetPetById;

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