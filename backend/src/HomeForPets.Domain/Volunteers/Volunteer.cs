using CSharpFunctionalExtensions;
using HomeForPets.Domain.Enums;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;

namespace HomeForPets.Domain.Volunteers;

public class Volunteer : Shared.Entity<VolunteerId>
{
    private readonly List<PaymentDetails> _paymentDetails = [];
    private readonly List<Pet> _pets = [];
    private readonly IReadOnlyList<SocialNetwork> _socialNetwork = [];

    //ef core
    private Volunteer(VolunteerId id) : base(id)
    {
    }
    private Volunteer(VolunteerId id,
        FullName fullName,
        PhoneNumber phoneNumber,
        Description description,
        int yearsOfExperience) : base(id)
    {
        FullName = fullName;
        Description = description;
        YearsOfExperience = yearsOfExperience;
        PhoneNumber = phoneNumber;
    }

    public FullName FullName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Description Description { get; private set; } = default!;
    public int? YearsOfExperience { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;

    public IReadOnlyList<PaymentDetails> PaymentDetailsList => _paymentDetails;
    public IReadOnlyList<SocialNetwork> SocialNetwork => _socialNetwork;


    public int? GetPetsHomeFoundCount() => _pets.Count(x => x.HelpStatus == HelpStatus.FoundHome);
    public int? GetPetsSearchForHomeCount() => _pets.Count(x => x.HelpStatus == HelpStatus.SearchHome);
    public int? GetPetsNeedForHelp() => _pets.Count(x => x.HelpStatus == HelpStatus.NeedForHelp);
    
    public void AddPaymentDetails(IEnumerable<PaymentDetails> paymentDetails) => _paymentDetails.AddRange(paymentDetails);
    public void AddPets(IEnumerable<Pet> pets) => _pets.AddRange(pets);

    public static Result<Volunteer,Error> Create(
        VolunteerId id,
        FullName fullName,
        PhoneNumber phoneNumber,
        Description description,
        int yearsOfExperience)
    {
        if (yearsOfExperience < 0)
        {
            return Errors.General.Validation("Years of experience");
        }

        var volunteer = new Volunteer(
            id,
            fullName,
            phoneNumber,
            description,
            yearsOfExperience);
        
        return volunteer;
    }
}