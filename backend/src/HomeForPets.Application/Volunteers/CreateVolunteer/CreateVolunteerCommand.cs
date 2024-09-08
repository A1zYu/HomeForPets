using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerCommand(
        FullNameDto FullNameDto,
        string Description,
        int WorkExperience,
        string PhoneNumber,
        IEnumerable<PaymentDetailsDto> PaymentDetails,
        IEnumerable<SocialNetworkDto> SocialNetworks);