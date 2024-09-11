using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Application.Database;
using HomeForPets.Application.Extensions;
using HomeForPets.Application.FileProvider;
using HomeForPets.Application.Validation;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.VolunteersManagement.Entities;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.Volunteers.AddPet;

public class AddPetHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPetCommand> _validator;

    public AddPetHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IValidator<AddPetCommand> validator,
        ILogger<AddPetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var volunteerResult = await _volunteersRepository
                .GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error.ToErrorList();
            }

            var petId = PetId.NewId();

            var description = Description.Create(command.Description).Value;

            var petDetails = PetDetails.Create(
                command.PetDetailsDto.Color,
                command.PetDetailsDto.HealthInfo,
                command.PetDetailsDto.Weight,
                command.PetDetailsDto.Height,
                command.PetDetailsDto.IsVaccinated,
                command.PetDetailsDto.IsNeutered,
                command.PetDetailsDto.BirthOfDate).Value;

            var address = Address.Create(
                command.AddressDto.City,
                command.AddressDto.Street,
                command.AddressDto.HouseNumber,
                command.AddressDto.FlatNumber).Value;

            var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

            var paymentDetails = PaymentDetailsList
                .Create(command.PaymentDetailsDto
                    .Select(x => PaymentDetails.Create(x.Name, x.Description).Value));

            var speciesId = SpeciesId.NewId;
            var breedId = Guid.NewGuid();

            var speciesBreed = SpeciesBreed.Create(speciesId, breedId).Value;

            var pet = Pet.Create(
                petId,
                command.Name,
                description,
                petDetails,
                address,
                phoneNumber,
                command.HelpStatus,
                speciesBreed);
            if (pet.IsFailure)
            {
                return pet.Error.ToErrorList();
            }

            if (command.PaymentDetailsDto.Any())
            {
                pet.Value.AddPaymentDetails(paymentDetails);    
            }
            
            
            volunteerResult.Value.AddPet(pet.Value);
            
            await _unitOfWork.SaveChanges(cancellationToken);
            

            _logger.LogInformation("Pet = {petId} added to volunteer - {volunteerId}",
                petId, volunteerResult.Value.Id);
            
            transaction.Commit();

            return petId.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Can not add pet to volunteer - {id} in transaction", command.VolunteerId);

            transaction.Rollback();

            return Error.Failure($"Can not add pet to volunteer - {command.VolunteerId}", "module.issue.failure").ToErrorList();
        }
    }
}