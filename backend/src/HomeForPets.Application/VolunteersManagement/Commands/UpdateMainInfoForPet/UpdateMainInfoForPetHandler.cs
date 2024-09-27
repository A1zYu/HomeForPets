using CSharpFunctionalExtensions;
using FluentValidation;
using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Database;
using HomeForPets.Application.Dtos.Volunteers;
using HomeForPets.Application.Extensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.VolunteersManagement.Entities;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.VolunteersManagement.Commands.UpdateMainInfoForPet;

public class UpdateMainInfoForPetHandler : ICommandHandler<Guid, UpdateMainInfoForPetCommand>
{
    public readonly ILogger<UpdateMainInfoForPetHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateMainInfoForPetCommand> _validator;

    public UpdateMainInfoForPetHandler(ILogger<UpdateMainInfoForPetHandler> logger,
        IVolunteersRepository volunteersRepository, IUnitOfWork unitOfWork,
        IValidator<UpdateMainInfoForPetCommand> validator)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateMainInfoForPetCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var volunteer = await _volunteersRepository.GetById(command.VolunteerId, ct);
        if (volunteer.IsFailure)
        {
            return volunteer.Error.ToErrorList();
        }

        var pet = volunteer.Value.Pets.FirstOrDefault(x => x.Id.Value == command.PetId);
        if (pet is null)
        {
            return Errors.General.NotFound(command.PetId).ToErrorList();
        }

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

        var speciesId = SpeciesId.Create(command.SpecialId);

        var breedId = BreedId.Create(command.BreedId);

        var specialBreed = SpeciesBreed.Create(speciesId, breedId).Value;

        var updatePet = Pet.Create(
            pet.Id,
            command.Name,
            description,
            petDetails,
            address,
            phoneNumber,
            command.HelpStatus,
            specialBreed);
        if (updatePet.IsFailure)
        {
            return Errors.General.ValueIsRequired("update pet").ToErrorList();
        }

        if (command.PaymentDetailsDto.Any())
        {
            var paymentsDetails = command.PaymentDetailsDto
                .Select(p => PaymentDetails.Create(p.Name, p.Description).Value);

            updatePet.Value.AddPaymentDetails(paymentsDetails);
        }

        var result = volunteer.Value.UpdateMainInfoForPet(updatePet.Value);
        if (result.IsFailure)
        {
            return result.Error.ToErrorList();
        }

        _logger.LogInformation($"Pet : {pet.Id} updated successfully");

        await _unitOfWork.SaveChanges(ct);

        return updatePet.Value.Id.Value;
    }
}