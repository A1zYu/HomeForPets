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

    public static Result<Volunteer> Create(VolunteerId id, string firstName, string lastName, string middleName,
        string description,
        int yearsOfExperience, PhoneNumber phoneNumber, List<Pet.Pet> pets,
        List<SocialNetwork> socialNetworks, List<PaymentDetails> paymentDetails)
    {
        var contact = Contact.Create(phoneNumber.Number, socialNetworks);
        if (contact.IsFailure)
        {
            return Result.Failure<Volunteer>("Contact in not correct");
        }

        var fullName = FullName.Create(firstName, lastName, middleName);
        if (fullName.IsFailure)
        {
            return Result.Failure<Volunteer>("Full name in not correct");
        }

        var volunteer = new Volunteer(
            id,
            fullName.Value,
            description,
            yearsOfExperience,
            contact.Value,
            pets,
            socialNetworks,
            paymentDetails);
        return Result.Success(volunteer);
    }
}