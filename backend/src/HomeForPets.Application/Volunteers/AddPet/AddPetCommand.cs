using HomeForPets.Application.Dtos;
using HomeForPets.Domain.Enums;

namespace HomeForPets.Application.Volunteers.AddPet;

public record AddPetCommand(
    Guid VolunteerId,
    string Name,
    string Description,
    PetDetailsDto PetDetailsDto, 
    AddressDto AddressDto,
    string PhoneNumber,
    HelpStatus HelpStatus,
    IEnumerable<PaymentDetailsDto> PaymentDetailsDto);
    
public record CreateFileCommand(Stream Content, string FileName);