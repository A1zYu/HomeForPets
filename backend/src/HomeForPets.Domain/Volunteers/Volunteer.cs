using CSharpFunctionalExtensions;
using HomeForPets.Domain.Enums;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;

namespace HomeForPets.Domain.Volunteers;

public class Volunteer : Shared.Entity<VolunteerId> , ISoftDeletable
{
    private readonly List<Pet> _pets = [];

    private bool _idDeleted = false;
    //ef core
    private Volunteer(VolunteerId id) : base(id) { }
    private Volunteer(VolunteerId id,
        FullName fullName,
        PhoneNumber phoneNumber,
        Description description,
        YearsOfExperience yearsOfExperience) : base(id)
    {
        FullName = fullName;
        Description = description;
        YearsOfExperience = yearsOfExperience;
        PhoneNumber = phoneNumber;
    }

    public FullName FullName { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Description Description { get; private set; } = default!;
    public YearsOfExperience YearsOfExperience { get; private set; }
    public PaymentDetailsList? PaymentDetailsList { get; private set; } 
    public SocialNetworkList? SocialNetworkList { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;
    public int? GetPetsHomeFoundCount() => _pets.Count(x => x.HelpStatus == HelpStatus.FoundHome);
    public int? GetPetsSearchForHomeCount() => _pets.Count(x => x.HelpStatus == HelpStatus.SearchHome);
    public int? GetPetsNeedForHelp() => _pets.Count(x => x.HelpStatus == HelpStatus.NeedForHelp);

    public void Delete()
    {
        _idDeleted = true;
    }
    public void Restore()
    {
        _idDeleted = false;
    }
    public void AddPets(IEnumerable<Pet> pets) => _pets.AddRange(pets);
    public void AddSocialNetworks(SocialNetworkList list) => SocialNetworkList = list;
    public void AddPaymentDetails(PaymentDetailsList paymentDetails) => PaymentDetailsList = paymentDetails;
    public void UpdateMainInfo(
        FullName fullName,
        Description description,
        YearsOfExperience yearsOfExperience,
        PhoneNumber phoneNumber)
    {
        FullName = fullName;
        Description = description;
        YearsOfExperience = yearsOfExperience;
        PhoneNumber = phoneNumber;
    }

    public static Result<Volunteer,Error> Create(
        VolunteerId id,
        FullName fullName,
        PhoneNumber phoneNumber,
        Description description,
        YearsOfExperience yearsOfExperience)
    {
        var volunteer = new Volunteer(
            id,
            fullName,
            phoneNumber,
            description,
            yearsOfExperience);
        
        return volunteer;
    }
}