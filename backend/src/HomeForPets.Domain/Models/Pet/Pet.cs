using CSharpFunctionalExtensions;
using HomeForPets.Domain.Enums;
using HomeForPets.Domain.ValueObjects;

namespace HomeForPets.Domain.Models.Pet;

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
        string species,
        string description,
        string breed,
        string color,
        string healthInfo,
        Address address,
        double weight,
        double height,
        PhoneNumber phoneNumberOwner,
        bool isNeutered,
        DateOnly birthOfDate,
        bool isVaccinated,
        HelpStatus helpStatus,
        List<PetPhoto> petPhotos,
        List<PaymentDetails> paymentDetails) : base(id)
    {
        Name = name;
        Species = species;
        Description = description;
        Breed = breed;
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
        _petPhotos = petPhotos;
        _paymentDetails = paymentDetails;
    }

    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string Color { get; private set; } = default!;
    public string HealthInfo { get; private set; } = default!;
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public string Species { get; private set; } = default!;
    public string Breed { get; private set; } = default!;
    public bool IsVaccinated { get; private set; }
    public bool IsNeutered { get; private set; }
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

    public static Result<Pet> Create(PetId id,
        string name,
        string species,
        string description,
        string breed,
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
        List<PetPhoto> petPhotos,
        List<PaymentDetails> paymentDetails
    )
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Pet>("Name can not be empty");
        }
        if (birthOfDate.Date > DateTime.Now.Date)
        {
            return Result.Failure<Pet>("Date is invalid");
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure<Pet>("Description can not be empty");
        }
        if (string.IsNullOrWhiteSpace(color))
        {
            return Result.Failure<Pet>("Color can not be empty");
        }

        if (string.IsNullOrWhiteSpace(healthInfo))
        {
            return Result.Failure<Pet>("Health information can not be empty");
        }

        if (weight <= 0)
        {
            return Result.Failure<Pet>("Weight must be greater than zero");
        }

        if (height <= 0)
        {
            return Result.Failure<Pet>("Height must be greater than zero");
        }

        
        var pet = new Pet(
            id,
            name,
            species,
            description,
            breed,
            color,
            healthInfo,
            address,
            weight,
            height,
            phoneNumberOwner,
            isNeutered,
            DateOnly.FromDateTime(birthOfDate),
            isVaccinated,
            helpStatus,
            petPhotos,
            paymentDetails
        );
        return Result.Success(pet);
    }
}