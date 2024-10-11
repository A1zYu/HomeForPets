using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.Core.Abstaction;
using HomeForPets.Core.Ids;
using HomeForPets.Species.Application.SpeciesManagement.Commands.CreateSpecies;
using HomeForPets.Species.Domain;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Species.Application.SpeciesManagement.Commands.AddBreed;

public class AddBreedHandler : ICommandHandler<Guid,AddBreedCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<CreateSpeciesHandler> _logger;

    public AddBreedHandler(IUnitOfWork unitOfWork, 
        ISpeciesRepository speciesRepository,
        ILogger<CreateSpeciesHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _speciesRepository = speciesRepository;
        _logger = logger;
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