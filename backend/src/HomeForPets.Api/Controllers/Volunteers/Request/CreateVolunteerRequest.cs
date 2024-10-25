using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.Volunteers;
using HomeForPets.Application.VolunteersManagement.Commands.CreateVolunteer;

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
    public CreateVolunteerCommand ToCommand()=>new (
        new FullNameDto(FirstName,LastName,MiddleName),
        Description,
        WorkExperience,
        PhoneNumber,
        PaymentDetails,
        SocialNetworks);
}