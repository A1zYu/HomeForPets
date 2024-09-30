using FluentValidation;
using HomeForPets.Application.Validation;
using HomeForPets.Domain.Shared;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;

namespace HomeForPets.Application.VolunteersManagement.Commands.UpdateMainInfoForPet;

public class UpdateMainInfoForPetValidator : AbstractValidator<UpdateMainInfoForPetCommand>
{
    public UpdateMainInfoForPetValidator()
    {
        RuleFor(p => p.PetId).NotNull().WithError(Error.NullValue);
        RuleFor(p => p.BreedId).NotNull().WithError(Error.NullValue);
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithError(Error.NullValue);
        RuleFor(p => p.Description)
            .MustBeValueObject(Description.Create);
        RuleForEach(p => p.PaymentDetailsDto)
            .MustBeValueObject(p => PaymentDetails.Create(p.Name, p.Description));
        RuleFor(p => p.AddressDto)
            .MustBeValueObject(a => Address.Create(a.City,a.Street,a.HouseNumber,a.FlatNumber));
        RuleFor(p => p.PetDetailsDto)
            .MustBeValueObject(pD => 
                PetDetails.Create(
                    pD.Color,
                    pD.HealthInfo,
                    pD.Weight,
                    pD.Height,
                    pD.IsVaccinated,
                    pD.IsNeutered,
                    pD.BirthOfDate));
        RuleFor(p => p.HelpStatus).NotEmpty().WithError(Error.None);
        RuleFor(p => p.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(p => p.VolunteerId).NotEmpty().WithError(Error.NullValue);
    }
}