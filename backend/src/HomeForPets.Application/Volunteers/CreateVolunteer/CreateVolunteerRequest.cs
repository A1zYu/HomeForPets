using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
        string FirstName,
        string LastName,
        string? MiddleName,
        string Description,
        int WorkExperience,
        string PhoneNumber,
        IEnumerable<SocialNetworkDto> SocialNetworks,
        IEnumerable<PaymentDetailsDto> PaymentDetailsList);