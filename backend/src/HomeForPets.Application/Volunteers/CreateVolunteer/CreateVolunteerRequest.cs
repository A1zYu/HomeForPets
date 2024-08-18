using HomeForPets.Domain.Models.PetModel;
using HomeForPets.Domain.DTOs;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
        string FirstName,
        string LastName,
        string? MiddleName,
        string Description,
        int WorkExperience,
        string PhoneNumber,
        List<SocialNetworkDto> SocialNetworks,
        List<PaymentDetailsDto> PaymentDetailsList);