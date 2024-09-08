using CSharpFunctionalExtensions;
using HomeForPets.Domain.Enums;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.Shared.ValueObjects;
using HomeForPets.Domain.Species;

namespace HomeForPets.Domain.Volunteers;

public class Pet : Shared.Entity<PetId>, ISoftDeletable
{
    private readonly List<PetPhoto> _petPhotos = [];
    private bool _idDeleted;

    //ef core
    private Pet(PetId id) : base(id) {}

    public Pet(PetId id,
        string name,
        Description description,
        Address address,
        PhoneNumber phoneNumberOwner,
        SpeciesBreed speciesBreed,
        HelpStatus helpStatus, 
        PetDetails petDetails) : base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        PhoneNumberOwner = phoneNumberOwner;
        HelpStatus = helpStatus;
        CreatedDate = DateOnly.FromDateTime(DateTime.Now);
        SpeciesBreed = speciesBreed;
        PetDetails = petDetails;
    }

    public string Name { get; private set; } = default!;
    public Description Description { get; private set; }
    public PetDetails PetDetails { get; private set; }
    public SpeciesBreed SpeciesBreed { get; private set; }
    public HelpStatus HelpStatus { get; private set; }

    public Address Address { get; private set; } = default!;

    public PhoneNumber PhoneNumberOwner { get; private set; } = default!;
    public DateOnly CreatedDate { get; private set; }

    public PaymentDetailsList? PaymentDetailsList { get; private set; }

    public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;
    public void AddPetPhotos(IEnumerable<PetPhoto> petPhotos) => _petPhotos.AddRange(petPhotos);
    public void AddPaymentDetails(PaymentDetailsList paymentDetails) => PaymentDetailsList = paymentDetails;

    public void Delete()
    {
        _idDeleted = true;
    }
    public void Restore()
    {
        _idDeleted = false;
    }
    public static Result<Pet, Error> Create(PetId id,
        string name,
        Description description,
        PetDetails petDetails,
        Address address,
        PhoneNumber phoneNumberOwner,
        HelpStatus helpStatus,
        SpeciesBreed speciesBreed
    )
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constraints.Constraints.LOW_VALUE_LENGTH)
        {
            return Errors.General.Validation("Pet name");
        }

        var pet = new Pet(
            id,
            name,
            description,
            address,
            phoneNumberOwner,
            speciesBreed,
            helpStatus,
            petDetails
        );
        return pet;
    }
}