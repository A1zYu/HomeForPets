using CSharpFunctionalExtensions;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.Shared.Ids;
using HomeForPets.Domain.VolunteersManagement.Entities;
using HomeForPets.Domain.VolunteersManagement.Enums;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;

namespace HomeForPets.Domain.VolunteersManagement;

public class Volunteer : Shared.Entity<VolunteerId>, ISoftDeletable
{
    private readonly List<Pet> _pets = [];
    private List<PaymentDetails> _paymentDetails=[];
    private List<SocialNetwork> _socialNetworks=[];

    private bool _isDeleted = false;

    //ef core
    private Volunteer(VolunteerId id) : base(id)
    {
    }

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
    public IReadOnlyList<PaymentDetails> PaymentDetails => _paymentDetails;
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<Pet> Pets => _pets;
    public int? GetPetsHomeFoundCount() => _pets.Count(x => x.HelpStatus == HelpStatus.FoundHome);
    public int? GetPetsSearchForHomeCount() => _pets.Count(x => x.HelpStatus == HelpStatus.SearchHome);
    public int? GetPetsNeedForHelp() => _pets.Count(x => x.HelpStatus == HelpStatus.NeedForHelp);

    public void Delete()
    {
        _isDeleted = true;
        foreach (var pet in _pets)
            pet.Delete();
    }

    public void Restore()
    {
        _isDeleted = false;
        foreach (var pet in _pets)
            pet.Restore();
    }

    public void AddSocialNetworks(IEnumerable<SocialNetwork> socialNetworks) =>  _socialNetworks.AddRange(socialNetworks);
    public void AddPaymentDetails(IEnumerable<PaymentDetails> paymentDetails)=> _paymentDetails.AddRange(paymentDetails.ToList());

    public Result<Pet, Error> GetPetById(PetId petId)
    {
        var pet = _pets.FirstOrDefault(i => i.Id == petId);
        if (pet is null)
            return Errors.General.NotFound(petId.Value);

        return pet;
    }

    public UnitResult<Error> AddPet(Pet pet)
    {
        var position = Position.Create(_pets.Count + 1);
        if (position.IsFailure)
        {
            return position.Error;
        }

        pet.SetPosition(position.Value);

        _pets.Add(pet);

        return Result.Success<Error>();
    }

    public UnitResult<Error> UpdateInfoPet(Pet updatedPet)
    {
        var pet = _pets.FirstOrDefault(i => i.Id == updatedPet.Id);
        if (pet is null)
        {
            return Errors.General.NotFound(updatedPet.Id);
        }

        pet.UpdateInfo(updatedPet);
        
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> MovePet(Position newPosition, Pet pet)
    {
        var currentPosition = pet.Position;

        if (currentPosition == newPosition)
        {
            return Result.Success<Error>();
        }

        var adjustedPositionResult = AdjustNewPositionOutOfRange(newPosition);
        if (adjustedPositionResult.IsFailure)
            return adjustedPositionResult.Error;
        
        newPosition = adjustedPositionResult.Value;
        
        var moveResult = MovePetsBetweenPositions(currentPosition, newPosition);
        if (moveResult.IsFailure)
            return moveResult.Error;
        
        pet.SetPosition(newPosition);
        
        return Result.Success<Error>();
    }
    private UnitResult<Error> MovePetsBetweenPositions(Position currentPosition, Position newPosition)
    {
        if (newPosition.Value < currentPosition.Value)
        {
            var petsToMove = _pets.Where(i => i.Position.Value >= newPosition.Value
                                                  && i.Position.Value < currentPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveForward();
                if (result.IsFailure)
                    return result.Error;
            }
        }
        else if (newPosition.Value > currentPosition.Value)
        {
            var petsToMove = _pets.Where(i => i.Position.Value > currentPosition.Value
                                                  && i.Position.Value <= newPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveBack();
                if (result.IsFailure)
                    return result.Error;
            }
        }

        return Result.Success<Error>();
    }

    private Result<Position, Error> AdjustNewPositionOutOfRange(Position newPosition)
    {
        if (newPosition.Value <= _pets.Count)
        {
            return newPosition;
        }
        var lastPosition = Position.Create(_pets.Count - 1);
        if (lastPosition.IsFailure)
            return lastPosition.Error;

        return lastPosition.Value;
        
    }

    public UnitResult<Error> DeletePetPhoto(PetId petId,PetPhotoId petPhotoId)
    {
        var pet = _pets.FirstOrDefault(i => i.Id == petId);
        if (pet is null)
        {
            return Errors.General.NotFound(petId.Value);
        }
        var result = pet.DeletePhoto(petPhotoId);
        if (result.IsFailure)
        {
            return result.Error;
        }
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> SetPetStatus(PetId id, HelpStatus helpStatus)
    {
        var pet = _pets.FirstOrDefault(i => i.Id == id);
        if (pet is null)
        {
            return Errors.General.NotFound(id);
        }
        
        pet.SetHelpStatus(helpStatus);
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> DeletePet(PetId petId)
    {
        var pet = _pets.FirstOrDefault(i => i.Id == petId);
        if (pet is null)
        {
            return Errors.General.NotFound(petId.Value);
        }
        
        pet.Delete();
        
        return UnitResult.Success<Error>();
    }

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

    public static Result<Volunteer, Error> Create(
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