using HomeForPets.Domain.Models.PetModel;
using HomeForPets.Domain.ValueObjects;

namespace HomeForPets.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
        string FirstName,
        string LastName,
        string? MiddleName,
        string Description,
        int WorkExperience,
        string PhoneNumber,
        List<SocialNetwork> SocialNetworks,
        List<Pet> Pets,
        List<PaymentDetails> PaymentDetailsList);