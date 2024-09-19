using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Dtos;

namespace HomeForPets.Application.VolunteersManagement.Commands.CreateVolunteer;

public record CreateVolunteerCommand(
        FullNameDto FullNameDto,
        string Description,
        int WorkExperience,
        string PhoneNumber,
        IEnumerable<PaymentDetailsDto> PaymentDetails,
        IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;