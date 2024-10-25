using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Dtos.Volunteers;
using HomeForPets.Domain.VolunteersManagement.Enums;

namespace HomeForPets.Application.VolunteersManagement.Commands.UpdateMainInfoForPet;

public record UpdateMainInfoForPetCommand(
    Guid VolunteerId,
    Guid PetId,
    string Name,
    string Description,
    PetDetailsDto PetDetailsDto,
    AddressDto AddressDto,
    string PhoneNumber,
    HelpStatus HelpStatus,
    Guid SpecialId,
    Guid BreedId,
    IEnumerable<PaymentDetailsDto> PaymentDetailsDto) : ICommand;