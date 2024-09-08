using HomeForPets.Application.Dtos;
using HomeForPets.Application.Volunteers.CreateVolunteer;

namespace HomeForPets.Api.Controllers.Volunteers.Request;

public record CreateVolunteerRequest(
    string FirstName,
    string LastName,
    string MiddleName,
    string Description,
    int WorkExperience,
    string PhoneNumber,
    IEnumerable<PaymentDetailsDto> PaymentDetails,
    IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public CreateVolunteerCommand ToCommand()=>new CreateVolunteerCommand(
        new FullNameDto(FirstName,LastName,MiddleName),
        Description,
        WorkExperience,
        PhoneNumber,
        PaymentDetails,
        SocialNetworks);
}