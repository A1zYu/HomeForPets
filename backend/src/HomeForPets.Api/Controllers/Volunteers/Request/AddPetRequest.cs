﻿using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.Volunteers;
using HomeForPets.Application.VolunteersManagement.Commands.AddPet;
using HomeForPets.Domain.VolunteersManagement.Enums;

namespace HomeForPets.Api.Controllers.Volunteers.Request;

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