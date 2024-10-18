using HomeForPets.Core.Abstactions;
using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.Core.Enums;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.AddPet;

public record AddPetCommand(
    Guid VolunteerId,
    string Name,
    string Description,
    PetDetailsDto PetDetailsDto, 
    AddressDto AddressDto,
    string PhoneNumber,
    HelpStatus HelpStatus,
    Guid SpecialId,
    Guid BreedId,
    IEnumerable<PaymentDetailsDto> PaymentDetailsDto) : ICommand;