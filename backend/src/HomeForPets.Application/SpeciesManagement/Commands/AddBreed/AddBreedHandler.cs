using CSharpFunctionalExtensions;
using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Database;
using HomeForPets.Application.SpeciesManagement.Commands.CreateSpecies;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Species;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.SpeciesManagement.Commands.AddBreed;

public class AddBreedHandler : ICommandHandler<Guid,AddBreedCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<CreateSpeciesHandler> _logger;
    private readonly IReadDbContext _readDbContext;

    public AddBreedHandler(IUnitOfWork unitOfWork, ISpeciesRepository speciesRepository, ILogger<CreateSpeciesHandler> logger, IReadDbContext readDbContext)
    {
        _unitOfWork = unitOfWork;
        _speciesRepository = speciesRepository;
        _logger = logger;
        _readDbContext = readDbContext;
    }

    public async Task<Result<Guid, ErrorList>> Handle(AddBreedCommand command, CancellationToken ct)
    {
        var species =await _speciesRepository.GetById(command.SpeciesId,ct);
        if (species.IsFailure)
        {
            return species.Error.ToErrorList();
        }

        var breedId = BreedId.NewId;
        var breed = Breed.Create(breedId, command.Name);
        if (breed.IsFailure)
        {
            return breed.Error.ToErrorList();
        }
        var result= species.Value.AddBreed(breed.Value);
        if (result.IsFailure)
        {
            return result.Error;
        }

        await _unitOfWork.SaveChanges(ct);
        
        return breedId.Value;
    }
}