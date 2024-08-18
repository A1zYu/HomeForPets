using CSharpFunctionalExtensions;
using HomeForPets.Domain.Enums;
using HomeForPets.Domain.Models.PetModel;
using HomeForPets.Domain.ValueObjects;

namespace HomeForPets.Domain.Models.Volunteer;

public class Volunteer : Shared.Entity<VolunteerId>
{
    private readonly List<PaymentDetails> _paymentDetails = [];
    private readonly List<Pet> _pets = [];

    //ef core
    private Volunteer(VolunteerId id) : base(id)
    {
    }

    private Volunteer(VolunteerId id,
        FullName fullName,
        string description,
        int yearsOfExperience,
        Contact contact) : base(id)
    {
        FullName = fullName;
        Description = description;
        YearsOfExperience = yearsOfExperience;
        Contact = contact;
    }

    public FullName FullName { get; private set; }
    public string Description { get; private set; } = default!;
    public int? YearsOfExperience { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;

    public IReadOnlyList<PaymentDetails> PaymentDetailsList => _paymentDetails;
    public Contact? Contact { get; private set; }


    public int? GetPetsHomeFoundCount() => _pets.Count(x => x.HelpStatus == HelpStatus.FoundHome);
    public int? GetPetsSearchForHomeCount() => _pets.Count(x => x.HelpStatus == HelpStatus.SearchHome);
    public int? GetPetsNeedForHelp() => _pets.Count(x => x.HelpStatus == HelpStatus.NeedForHelp);


    public void AddPaymentDetails(PaymentDetails paymentDetails) => _paymentDetails.Add(paymentDetails);
    public void AddPetPhotos(Pet pet) => _pets.Add(pet);

    public static Result<Volunteer> Create(VolunteerId id, FullName fullName,
        string description, Contact contact,
        int yearsOfExperience)
    {
        if (string.IsNullOrWhiteSpace(description) || description.Length > Constraints.Constraints.HIGH_VALUE_LENGTH)
            return Result.Failure<Volunteer>("Description is not correct");

        var volunteer = new Volunteer(
            id,
            fullName,
            description,
            yearsOfExperience,
            contact);
        return Result.Success(volunteer);
    }
}