using CSharpFunctionalExtensions;
using HomeForPets.Domain.Enums;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.ValueObjects;

namespace HomeForPets.Domain.Models.PetModel;

public class Pet : Shared.Entity<PetId>
{
    private readonly List<PaymentDetails> _paymentDetails = [];
    private readonly List<PetPhoto> _petPhotos = [];

    //ef core
    private Pet(PetId id) : base(id)
    {
    }

    public Pet(
        PetId id,
        string name,
        string description,
        string color,
        string healthInfo,
        Address address,
        double weight,
        double height,
        PhoneNumber phoneNumberOwner,
        bool isNeutered,
        DateOnly birthOfDate,
        bool isVaccinated,
        SpeciesBreed speciesBreed,
        HelpStatus helpStatus,
        List<PetPhoto> petPhotos,
        List<PaymentDetails> paymentDetails) : base(id)
    {
        Name = name;
        Description = description;
        Color = color;
        HealthInfo = healthInfo;
        Address = address;
        Weight = weight;
        Height = height;
        PhoneNumberOwner = phoneNumberOwner;
        IsNeutered = isNeutered;
        BirthOfDate = birthOfDate;
        IsVaccinated = isVaccinated;
        HelpStatus = helpStatus;
        CreatedDate = DateOnly.FromDateTime(DateTime.Now);
        SpeciesBreed = speciesBreed;
        _petPhotos = petPhotos;
        _paymentDetails = paymentDetails;
    }

    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string Color { get; private set; } = default!;
    public string HealthInfo { get; private set; } = default!;
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public bool IsVaccinated { get; private set; }
    public bool IsNeutered { get; private set; }
    public SpeciesBreed SpeciesBreed { get; private set; }
    public DateOnly BirthOfDate { get; private set; }
    public HelpStatus HelpStatus { get; private set; }

    public Address Address { get; private set; } = default!;

    public PhoneNumber PhoneNumberOwner { get; private set; } = default!;

    public IReadOnlyList<PaymentDetails> PaymentDetailsList => _paymentDetails;

    public DateOnly CreatedDate { get; private set; }

    public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;


    public void AddPaymentDetails(PaymentDetails paymentDetails) => _paymentDetails.Add(paymentDetails);
    public void AddPetPhotos(PetPhoto petPhotos) => _petPhotos.Add(petPhotos);
    public void SetVaccinated() => IsVaccinated = !IsVaccinated;
    public void SetCastrated() => IsNeutered = !IsNeutered;

    public static Result<Pet,Error> Create(PetId id,
        string name,
        string description,
        string color,
        string healthInfo,
        Address address,
        double weight,
        double height,
        PhoneNumber phoneNumberOwner,
        bool isNeutered,
        DateTime birthOfDate,
        bool isVaccinated,
        HelpStatus helpStatus,
        SpeciesBreed speciesBreed,
        List<PetPhoto> petPhotos,
        List<PaymentDetails> paymentDetails
    )
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Errors.General.Validation("Pet name");
        }
        if (birthOfDate.Date > DateTime.Now.Date)
        {
            return Errors.General.Validation("Birth Of Date");
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            return Errors.General.Validation("Description");
        }
        if (string.IsNullOrWhiteSpace(color))
        {
            return Errors.General.Validation("Color");
        }

        if (string.IsNullOrWhiteSpace(healthInfo))
        {
            return Errors.General.Validation("Health info");
        }

        if (weight <= 0)
        {
            return Errors.General.Validation("Weight");
        }

        if (height <= 0)
        {
            return Errors.General.Validation("Height");

        }

        
        var pet = new Pet(
            id,
            name,
            description,
            color,
            healthInfo,
            address,
            weight,
            height,
            phoneNumberOwner,
            isNeutered,
            DateOnly.FromDateTime(birthOfDate),
            isVaccinated,
            speciesBreed,
            helpStatus,
            petPhotos,
            paymentDetails
        );
        return pet;
    }
}