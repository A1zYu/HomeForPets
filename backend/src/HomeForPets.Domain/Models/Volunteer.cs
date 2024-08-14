using CSharpFunctionalExtensions;
using HomeForPets.Shared;
using HomeForPets.ValueObject;

namespace HomeForPets.Models;

public class Volunteer : Shared.Entity<VolunteerId>
{
    private readonly List<PaymentDetails> _paymentDetails = [];
    private readonly List<Pet> _pets = [];
    private readonly List<SocialNetwork> _socialNetworks = [];

    //ef core
    private Volunteer(VolunteerId id) : base(id) {}

    public Volunteer(VolunteerId id,FullName fullName, string description, int yearsOfExperience, int petHomeFoundCount,
        int petSearchForHomeCount, int petTreatmentCount, string phoneNumber, List<Pet> pets,
        List<SocialNetwork> socialNetworks, List<PaymentDetails> paymentDetails) : base(id)
    {
        
        FullName = fullName;
        Description = description;
        YearsOfExperience = yearsOfExperience;
        PetHomeFoundCount = petHomeFoundCount;
        PetSearchForHomeCount = petSearchForHomeCount;
        PetTreatmentCount = petTreatmentCount;
        PhoneNumber = phoneNumber;
        _pets = pets;
        _socialNetworks = socialNetworks;
        _paymentDetails = paymentDetails;
    }

    public FullName FullName { get; private set; }
    public string Description { get; private set; } = default!;
    public int YearsOfExperience { get; private set; }
    public int PetHomeFoundCount { get; private set; }
    public int PetSearchForHomeCount { get; private set; }
    public int PetTreatmentCount { get; private set; }
    public string PhoneNumber { get; private set; } = default!;
    public IReadOnlyList<PaymentDetails> PaymentDetailsList => _paymentDetails;
    public IReadOnlyList<Pet> Pets => _pets;
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;


    public void AddPaymentDetails(PaymentDetails paymentDetails) => _paymentDetails.Add(paymentDetails);
    public void AddPetPhotos(Pet pet) => _pets.Add(pet);
    public void AddSocialNetwork(SocialNetwork socialNetwork) => _socialNetworks.Add(socialNetwork);

    public static Result<Volunteer> Create(VolunteerId id,string firstName, string lastName, string middleName, string description,
        int yearsOfExperience, int petHomeFoundCount,
        int petSearchForHomeCount, int petTreatmentCount, string phoneNumber, List<Pet> pets,
        List<SocialNetwork> socialNetworks, List<PaymentDetails> paymentDetails)
    {
        var fullName = FullName.Create(firstName, lastName, middleName);
        if (fullName.IsSuccess)
        {
            var volunteer = new Volunteer(id,fullName.Value, description, yearsOfExperience, petHomeFoundCount,
                petSearchForHomeCount, petTreatmentCount, phoneNumber, pets, socialNetworks, paymentDetails);
            return Result.Success(volunteer);
        }

        return Result.Failure<Volunteer>("Fullname can not empty");
    }
}