using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
        FullNameDto FullNameDto,
        string Description,
        int WorkExperience,
        string PhoneNumber,
        IEnumerable<PaymentDetailsDto> PaymentDetails,
        IEnumerable<SocialNetworkDto> SocialNetworks);