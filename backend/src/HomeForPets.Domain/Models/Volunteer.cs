using CSharpFunctionalExtensions;

namespace HomeForPets.Models;

public class Volunteer
{
    private readonly List<PaymentDetails> _paymentDetails = [];
    private readonly List<Pet> _pets=[];
    private readonly List<SocialNetwork> _socialNetworks=[];

    //ef core
    private Volunteer() { }

    private Volunteer(Guid guid, string fullName)
    {
        
    }
    
    public Guid Id { get; private set; }
    public string FullName { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public int YearsOfExperience { get; private set; }
    public int PetHomeFoundCount { get; private set; }
    public int PetSearchForHomeCount { get; private set; }
    public int PetTreatmentCount { get; private set; }
    public string PhoneNumber { get; private set; }
    public IReadOnlyList<PaymentDetails> PaymentDetailsList => _paymentDetails;
    public IReadOnlyList<Pet> Pets => _pets;
    public IReadOnlyList<SocialNetwork> SocialNetworks=> _socialNetworks;


    public Guid SetId => Guid.NewGuid();
    public void AddPaymentDetails(PaymentDetails paymentDetails) => _paymentDetails.Add(paymentDetails);
    public void AddPetPhotos(Pet pet) => _pets.Add(pet);
    public void AddSocialNetwork(SocialNetwork socialNetwork) => _socialNetworks.Add(socialNetwork);

    public Result<Volunteer> Create(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return Result.Failure<Volunteer>("FullName can not be empty");
        }
        var volunteer = new Volunteer(SetId, fullName);
        return Result.Success(volunteer);
    }
}
