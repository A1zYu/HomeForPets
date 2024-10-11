using HomeForPets.Core.Abstaction;
using HomeForPets.Core.Dtos.Volunteers;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.CreateVolunteer;

public record CreateVolunteerCommand(
        FullNameDto FullNameDto,
        string Description,
        int WorkExperience,
        string PhoneNumber,
        IEnumerable<PaymentDetailsDto> PaymentDetails,
        IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;