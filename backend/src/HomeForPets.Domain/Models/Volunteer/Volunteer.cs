using CSharpFunctionalExtensions;
using HomeForPets.Domain.Enums;
using HomeForPets.Domain.ValueObjects;

namespace HomeForPets.Domain.Models.Volunteer;

public class Volunteer : Shared.Entity<VolunteerId>
{
    private readonly List<PaymentDetails> _paymentDetails = [];
    private readonly List<Pet.Pet> _pets = [];
    private readonly List<SocialNetwork> _socialNetworks = [];

    //ef core
    private Volunteer(VolunteerId id) : base(id)
    {
    }

    public Volunteer(VolunteerId id,
        FullName fullName,
        string description,
        int yearsOfExperience,
        Contact contact,
        List<Pet.Pet> pets,
        List<SocialNetwork> socialNetworks,
        List<PaymentDetails> paymentDetails) : base(id)
    {
        FullName = fullName;
        Description = description;
        YearsOfExperience = yearsOfExperience;
        Contact = contact;
        _pets = pets;
        _socialNetworks = socialNetworks;
        _paymentDetails = paymentDetails;
    }

    public FullName FullName { get; private set; }
    public string Description { get; private set; } = default!;
    public int? YearsOfExperience { get; private set; }
    public IReadOnlyList<Pet.Pet> Pets => _pets;

    public IReadOnlyList<PaymentDetails> PaymentDetailsList => _paymentDetails;
    public Contact? Contact { get; private set; }


    public int? GetPetsHomeFoundCount() => _pets.Count(x => x.HelpStatus == HelpStatus.FoundHome);
    public int? GetPetsSearchForHomeCount() => _pets.Count(x => x.HelpStatus == HelpStatus.SearchHome);
    public int? GetPetsNeedForHelp() => _pets.Count(x => x.HelpStatus == HelpStatus.NeedForHelp);


    public void AddPaymentDetails(PaymentDetails paymentDetails) => _paymentDetails.Add(paymentDetails);
    public void AddPetPhotos(Pet.Pet pet) => _pets.Add(pet);
    public void AddSocialNetwork(SocialNetwork socialNetwork) => _socialNetworks.Add(socialNetwork);

    public static Result<Volunteer> Create(VolunteerId id, FullName fullName,
        string description, Contact contact,
        int yearsOfExperience, PhoneNumber phoneNumber, List<Pet.Pet> pets,
        List<SocialNetwork> socialNetworks, List<PaymentDetails> paymentDetails)
    {
        if (string.IsNullOrWhiteSpace(description) || description.Length > Constraints.Constraints.HIGH_VALUE_LENGTH)
            return Result.Failure<Volunteer>("Description is not correct");

        var volunteer = new Volunteer(
            id,
            fullName,
            description,
            yearsOfExperience,
            contact,
            pets,
            socialNetworks,
            paymentDetails);
        return Result.Success(volunteer);
    }
}