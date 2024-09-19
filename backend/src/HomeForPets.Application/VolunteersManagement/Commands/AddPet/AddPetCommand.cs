using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Dtos;
using HomeForPets.Domain.VolunteersManagement.Enums;

namespace HomeForPets.Application.VolunteersManagement.Commands.AddPet;

public record AddPetCommand(
    Guid VolunteerId,
    string Name,
    string Description,
    PetDetailsDto PetDetailsDto, 
    AddressDto AddressDto,
    string PhoneNumber,
    HelpStatus HelpStatus,
    IEnumerable<PaymentDetailsDto> PaymentDetailsDto) : ICommand;