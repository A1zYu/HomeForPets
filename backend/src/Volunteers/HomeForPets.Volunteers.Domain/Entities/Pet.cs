using CSharpFunctionalExtensions;
using HomeForPets.Core;
using HomeForPets.Core.Enums;
using HomeForPets.Core.Ids;
using HomeForPets.Volunteers.Domain.ValueObjects;

namespace HomeForPets.Volunteers.Domain.Entities;

public class Pet : Core.Entity<PetId>, ISoftDeletable
{
    private readonly List<PetPhoto> _petPhotos = [];
    private List<PaymentDetails> _paymentDetails = [];
    private bool _isDeleted;

    //ef core
    private Pet(PetId id) : base(id)
    {
    }

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
    public Position Position { get; private set; }
    public Address Address { get; private set; } = default!;

    public PhoneNumber PhoneNumberOwner { get; private set; } = default!;
    public DateOnly CreatedDate { get; private set; }

    // public PaymentDetailsList? PaymentDetailsList { get; private set; }

    public IReadOnlyList<PaymentDetails> PaymentDetails => _paymentDetails;
    public IReadOnlyList<PetPhoto> PetPhotos => _petPhotos;
    public void AddPetPhotos(IEnumerable<PetPhoto> petPhotos) => _petPhotos.AddRange(petPhotos);
    public void AddPaymentDetails(IEnumerable<PaymentDetails> paymentDetails) => _paymentDetails.AddRange(paymentDetails);

    public void Delete()
    {
        _isDeleted = true;
    }

    public void Restore()
    {
        _isDeleted = false;
    }

    public void SetPosition(Position position) =>
        Position = position;
    
    public UnitResult<Error> MoveForward()
    {
        var newPosition = Position.Forward();
        if(newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
    }

    public UnitResult<Error> MoveBack()
    {
        var newPosition = Position.Back();
        if(newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<Error>();
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
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.LOW_VALUE_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Pet name");
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

    internal void UpdateInfo(Pet updatedPet)
    {
        Name = updatedPet.Name;
        Description = updatedPet.Description;
        Address = updatedPet.Address;
        PhoneNumberOwner = updatedPet.PhoneNumberOwner;
        SpeciesBreed = updatedPet.SpeciesBreed;
        HelpStatus = updatedPet.HelpStatus;
        PetDetails = updatedPet.PetDetails;
        _paymentDetails = updatedPet.PaymentDetails.ToList();
    }

    internal UnitResult<Error> DeletePhoto(PetPhotoId petPhotoId)
    {
        var photo = _petPhotos.FirstOrDefault(p => p.Id == petPhotoId);
        if (photo is null)
        {
            return Errors.General.NotFound(petPhotoId.Value);
        }
        _petPhotos.Remove(photo);
        return UnitResult.Success<Error>();
    }

    internal void SetHelpStatus(HelpStatus helpStatus) => HelpStatus = helpStatus;

    public UnitResult<Error> SetMainPhoto(PetPhotoId id)
    {
        foreach (var petPhoto in _petPhotos)
        {
            if (petPhoto.IsMain)
            {
                petPhoto.SetMain();
            }
        }
        var photo = _petPhotos.FirstOrDefault(x => x.Id == id);
        if (photo is null)
        {
            return Errors.General.NotFound(id);
        }
        photo.SetMain();
        return UnitResult.Success<Error>();
    }
}