using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.Core.Enums;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.AddPet;

namespace HomeForPets.Volunteers.Controllers.Request;

public record AddPetRequest(
    string Name,
    string Description,
    PetDetailsDto PetDetailsDto,
    IEnumerable<PaymentDetailsDto> PaymentDetailsDto,
    AddressDto AddressDto,
    string PhoneNumber,
    Guid SpecialId,
    Guid BreedId,
    HelpStatus HelpStatus
)
{
    public AddPetCommand ToCommand(Guid volunteerId) => 
        new(
            volunteerId,
            Name,
            Description,
            PetDetailsDto,
            AddressDto,
            PhoneNumber,
            HelpStatus,
            SpecialId,
            BreedId,
            PaymentDetailsDto);
};