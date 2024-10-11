// using System.Reactive;
// using CommunityToolkit.HighPerformance.Helpers;
// using CSharpFunctionalExtensions;
// using HomeForPets.Application.SpeciesManagement;
// using HomeForPets.Domain.Species;
// using HomeForPets.Infrastructure.DbContexts;
// using HomeForPets.Core;
// using HomeForPets.Core.Ids;
// using Microsoft.EntityFrameworkCore;
//
// namespace HomeForPets.Infrastructure.Repositories;
//
// public class SpeciesRepository : ISpeciesRepository
// {
//     private readonly WriteDbContext _writeDbContext;
//
//     public SpeciesRepository(WriteDbContext writeDbContext)
//     {
//         _writeDbContext = writeDbContext;
//     }
//
//     public async Task<Guid> Add(Species species, CancellationToken ct = default)
//     {
//         await _writeDbContext.Species.AddAsync(species, ct);
//         return species.Id;
//     }
//
//     public async Task<Result<Species, Error>> GetById(SpeciesId speciesId, CancellationToken ct = default)
//     {
//         var species = await _writeDbContext.Species.Include(x => x.Breeds)
//             .FirstOrDefaultAsync(species => species.Id == speciesId, ct);
//         if (species is null)
//         {
//             return Errors.General.NotFound(speciesId);
//         }
//
//         return species;
//     }
//
//     public async Task<UnitResult<Error>> Delete(Species species, CancellationToken ct)
//     {
//         _writeDbContext.Species.Remove(species);
//
//         return UnitResult.Success<Error>();
//     }
//     
// }