﻿using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Database;
using HomeForPets.Application.Extensions;
using HomeForPets.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.SpeciesManagement.Commands.DeleteSpecies;

public class DeleteSpeciesHandler : ICommandHandler<DeleteSpeciesCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<DeleteSpeciesHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<DeleteSpeciesCommand> _validator;

    public DeleteSpeciesHandler(IUnitOfWork unitOfWork, ISpeciesRepository speciesRepository, ILogger<DeleteSpeciesHandler> logger, IReadDbContext readDbContext, IValidator<DeleteSpeciesCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _speciesRepository = speciesRepository;
        _logger = logger;
        _readDbContext = readDbContext;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(DeleteSpeciesCommand command, CancellationToken ct)
    {
        var validationResult =await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }
        
        var species =await _speciesRepository.GetById(command.SpeciesId,ct);
        if (species.IsFailure)
        {
            return species.Error.ToErrorList();
        }
       
        var petsSpeciesExit =await _readDbContext.Pets.AnyAsync(p=>p.SpeciesId==command.SpeciesId, ct);
        if (petsSpeciesExit)
        {
            return Errors.Pet.SpeciesExistsInPet(command.SpeciesId).ToErrorList();
        }
        
        var result = await _speciesRepository.Delete(species.Value, ct);
        if (result.IsFailure)
        {
            result.Error.ToErrorList();
        }

        await _unitOfWork.SaveChanges(ct);
        
        _logger.LogInformation($"Species {command.SpeciesId} deleted successfully.");
        
        return UnitResult.Success<ErrorList>();
    }
}