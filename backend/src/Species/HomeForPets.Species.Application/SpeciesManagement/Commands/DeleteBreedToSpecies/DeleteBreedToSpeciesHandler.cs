using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Abstactions;
using HomeForPets.Core.Extensions;
using HomeForPets.SharedKernel;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Species.Application.SpeciesManagement.Commands.DeleteBreedToSpecies;

public class DeleteBreedToSpeciesHandler : ICommandHandler<DeleteBreedToSpeciesCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<DeleteBreedToSpeciesHandler> _logger;
    // private readonly ISpeciesReadDbContext _readDbContext;
    private readonly IValidator<DeleteBreedToSpeciesCommand> _validator;

    public DeleteBreedToSpeciesHandler(IUnitOfWork unitOfWork, ISpeciesRepository speciesRepository, ILogger<DeleteBreedToSpeciesHandler> logger,
        // ISpeciesReadDbContext readDbContext,
        IValidator<DeleteBreedToSpeciesCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _speciesRepository = speciesRepository;
        _logger = logger;
        // _readDbContext = readDbContext;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(DeleteBreedToSpeciesCommand command, CancellationToken ct)
    {
        var validationResult =await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }
        
        // var isBreedUsed = await _readDbContext.Pets.AnyAsync(x=>x.BreedId == command.BreedId, ct);
        // if (isBreedUsed)
        // {
            // return Errors.General.AlreadyExist().ToErrorList();
        // }
        
        var species = await _speciesRepository.GetById(command.SpeciesId, ct);
        if (species.IsFailure)
        {
            return species.Error.ToErrorList();
        }
        
        var deleteBreed = species.Value.RemoveBreed(command.BreedId);
        if (deleteBreed.IsFailure)
        {
            return deleteBreed.Error.ToErrorList();
        }
        
        await _unitOfWork.SaveChanges(ct);
        
        _logger.LogInformation($"Successfully deleted breed {command.BreedId}");
        
        return UnitResult.Success<ErrorList>();
    }
}