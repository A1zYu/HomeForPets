using HomeForPets.Core.Abstaction;
using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.Core.Enums;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UpdateMainInfoForPet;

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