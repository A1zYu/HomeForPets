using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Abstactions;
using HomeForPets.Core.Extensions;
using HomeForPets.SharedKernel;
using HomeForPets.SharedKernel.Ids;
using HomeForPets.Volunteers.Domain.Entities;
using HomeForPets.Volunteers.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.AddPet;

public class AddPetHandler : ICommandHandler<Guid,AddPetCommand>
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
        
        var speciesId = SpeciesId.Create(command.SpecialId).Value;
        
        var breedId = BreedId.Create(command.BreedId).Value;

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
            var paymentDetails = command.PaymentDetailsDto
                    .Select(x => PaymentDetails.Create(x.Name, x.Description).Value);
            pet.Value.AddPaymentDetails(paymentDetails);
        }
        
        volunteerResult.Value.AddPet(pet.Value);

        await _unitOfWork.SaveChanges(cancellationToken);


        _logger.LogInformation("Pet = {petId} added to volunteer - {volunteerId}",
            petId, volunteerResult.Value.Id);

        return petId.Value;
    }
}