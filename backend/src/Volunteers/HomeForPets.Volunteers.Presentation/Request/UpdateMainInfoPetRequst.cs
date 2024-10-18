using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.Core.Enums;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UpdateMainInfoForPet;

namespace HomeForPets.Volunteers.Controllers.Request;

public record UpdateMainInfoPetRequst(
    Guid PetId,
    string Name,
    string Description,
    PetDetailsDto PetDetailsDto,
    IEnumerable<PaymentDetailsDto> PaymentDetailsDto,
    AddressDto AddressDto,
    string PhoneNumber,
    Guid SpecialId,
    Guid BreedId,
    HelpStatus HelpStatus)
{
    public UpdateMainInfoForPetCommand ToCommand(Guid volunteerId)=>
        new(volunteerId,
            PetId,
            Name,
            Description,
            PetDetailsDto,
            AddressDto,
            PhoneNumber,
            HelpStatus,
            SpecialId,
            BreedId,
            PaymentDetailsDto);
}