using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Database;
using HomeForPets.Application.Extensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Species;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.SpeciesManagement.Commands.CreateSpecies;

public class CreateSpeciesHandler : ICommandHandler<Guid,CreateSpeciesCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<CreateSpeciesHandler> _logger;
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<CreateSpeciesCommand> _validator;
    public CreateSpeciesHandler(IUnitOfWork unitOfWork, ISpeciesRepository speciesRepository, ILogger<CreateSpeciesHandler> logger, IReadDbContext readDbContext, IValidator<CreateSpeciesCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _speciesRepository = speciesRepository;
        _logger = logger;
        _readDbContext = readDbContext;
        _validator = validator;
    }
    public async Task<Result<Guid, ErrorList>> Handle(CreateSpeciesCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command,ct);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }
        
        var speciesId = SpeciesId.NewId;
        
        var speciesToAdd = Species.Create(speciesId,command.Name);

        var speciesExist =  _readDbContext.Species.Any(x=>x.Name==command.Name);
        if (speciesExist)
        {
            return Errors.General.AlreadyExist().ToErrorList();
        }

        if (speciesToAdd.IsFailure)
        {
            return speciesToAdd.Error.ToErrorList();
        }
        
        await _speciesRepository.Add(speciesToAdd.Value, ct);

        await _unitOfWork.SaveChanges(ct);
        
        _logger.LogInformation($"Added Species: {speciesToAdd.Value}");
     
        return speciesId.Value;
    }
}