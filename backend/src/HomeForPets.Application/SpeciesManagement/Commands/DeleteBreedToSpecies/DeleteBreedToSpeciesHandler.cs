using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Database;
using HomeForPets.Application.Extensions;
using HomeForPets.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.SpeciesManagement.Commands.DeleteBreedToSpecies;

public class DeleteBreedToSpeciesHandler : ICommandHandler<DeleteBreedToSpeciesCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<DeleteBreedToSpeciesHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<DeleteBreedToSpeciesCommand> _validator;

    public DeleteBreedToSpeciesHandler(IUnitOfWork unitOfWork, ISpeciesRepository speciesRepository, ILogger<DeleteBreedToSpeciesHandler> logger, IReadDbContext readDbContext, IValidator<DeleteBreedToSpeciesCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _speciesRepository = speciesRepository;
        _logger = logger;
        _readDbContext = readDbContext;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(DeleteBreedToSpeciesCommand command, CancellationToken ct)
    {
        var validationResult =await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }
        
        var isBreedUsed = await _readDbContext.Pets.AnyAsync(x=>x.BreedId == command.BreedId, ct);
        if (isBreedUsed)
        {
            return Errors.General.AlreadyExist().ToErrorList();
        }
        
        var species = await _speciesRepository.GetById(command.SpeciesId, ct);
        if (species.IsFailure)
        {
            return species.Error.ToErrorList();
        }

        var breed = species.Value.Breeds.FirstOrDefault(x => x.Id.Value == command.BreedId);
        if (breed is null)
        {
            return Errors.General.NotFound(breed?.Id.Value).ToErrorList();
        }
        
        
        await _speciesRepository.DeleteBreed(command.SpeciesId,command.BreedId,ct);
        
        var deleteBreed = species.Value.RemoveBreed(breed.Id);
        if (deleteBreed.IsFailure)
        {
            return deleteBreed.Error.ToErrorList();
        }

        await _unitOfWork.SaveChanges(ct);
        
        _logger.LogInformation($"Successfully deleted breed {command.BreedId}");
        
        return UnitResult.Success<ErrorList>();
    }
}