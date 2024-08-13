using CSharpFunctionalExtensions;
using HomeForPets.Enums;

namespace HomeForPets.Models;

public class Pet
{
    private readonly List<PaymentDetails> _paymentDetails = [];
    private List<PetPhoto> _petPhotos = [];

    //ef core
    private Pet()
    {
    }

    private Pet(Guid id, string name, string species, string description, string breed, string color, string healthInfo,
        string address, double weight, double height, string phoneNumber, bool isNeutered, DateOnly birthOfDate,
        bool isVaccinated, HelpStatus helpStatus, DateOnly createdDate)
    {
        Id = id;
        Name = name;
        Species = species;
        Description = description;
        Breed = breed;
        Color = color;
        HealthInfo = healthInfo;
        Address = address;
        Weight = weight;
        Height = height;
        PhoneNumber = phoneNumber;
        IsNeutered = isNeutered;
        BirthOfDate = birthOfDate;
        IsVaccinated = isVaccinated;
        HelpStatus = helpStatus;
        CreatedDate = createdDate;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = default!;

    public string Species { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public string Breed { get; private set; } = default!;

    public string Color { get; private set; } = default!;

    public string HealthInfo { get; private set; } = default!;

    public string Address { get; private set; } = default!;

    public double Weight { get; private set; }

    public double Height { get; private set; }

    public string PhoneNumber { get; private set; } = default!;

    public bool IsNeutered { get; private set; }

    public DateOnly BirthOfDate { get; private set; }

    public bool IsVaccinated { get; private set; }

    public HelpStatus HelpStatus { get; private set; }
    
    public Guid? VolunteerId { get; private set; }

    public IReadOnlyList<PaymentDetails> PaymentDetailsList => _paymentDetails;

    public DateOnly CreatedDate { get; private set; }

    public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;


    public void AddPaymentDetails(PaymentDetails paymentDetails) => _paymentDetails.Add(paymentDetails);
    public void AddPetPhotos(PetPhoto petPhotos) => _petPhotos.Add(petPhotos);
    public void SetVaccinated() => IsVaccinated = !IsVaccinated;
    public void SetCastrated() => IsNeutered = !IsNeutered;
    
    public Guid SetGuidId()  { return Guid.NewGuid(); } 
    public static Result<Pet> Create(Guid id, string name, string species, string description, string breed,
        string color, string healthInfo,
        string address, double weight, double height, string phoneNumber, bool isNeutered, DateOnly birthOfDate,
        bool isVaccinated, HelpStatus helpStatus, DateOnly createdDate)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Pet>("Name can not be empty");
        }
        if (string.IsNullOrWhiteSpace(species))
        {
            return Result.Failure<Pet>("Species can not be empty");
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure<Pet>("Description can not be empty");
        }
        if (string.IsNullOrWhiteSpace(breed))
        {
            return Result.Failure<Pet>("Breed can not be empty");
        }
        if (string.IsNullOrWhiteSpace(color))
        {
            return Result.Failure<Pet>("Color can not be empty");
        }
        if (string.IsNullOrWhiteSpace(healthInfo))
        {
            return Result.Failure<Pet>("Health information can not be empty");
        }
        if (string.IsNullOrWhiteSpace(address))
        {
            return Result.Failure<Pet>("Address can not be empty");
        }
        if (weight <= 0)
        {
            return Result.Failure<Pet>("Weight must be greater than zero");
        }
        if (height <= 0)
        {
            return Result.Failure<Pet>("Height must be greater than zero");
        }
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return Result.Failure<Pet>("Phone number can not be empty");
        }
    
        var pet = new Pet
        {
            _petPhotos = null,
            Id = Guid.NewGuid(),
            Name = name,
            Species = species,
            Description = description,
            Breed = breed,
            Color = color,
            HealthInfo = healthInfo,
            Address = address,
            Weight = weight,
            Height = height,
            PhoneNumber = phoneNumber,
            IsNeutered = isNeutered,
            BirthOfDate = birthOfDate,
            IsVaccinated = isVaccinated,
            HelpStatus = HelpStatus.NeedForHelp,
            CreatedDate = DateOnly.FromDateTime(DateTime.Now)
        };
        return Result.Success(pet);
    }

}