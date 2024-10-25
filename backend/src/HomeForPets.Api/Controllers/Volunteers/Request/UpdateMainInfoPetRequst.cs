using HomeForPets.Application.Dtos.Volunteers;
using HomeForPets.Application.VolunteersManagement.Commands.UpdateMainInfoForPet;
using HomeForPets.Domain.VolunteersManagement.Enums;

namespace HomeForPets.Api.Controllers.Volunteers.Request;

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