using FluentValidation;
using HomeForPets.Core;
using HomeForPets.Core.Validation;
using HomeForPets.Volunteers.Domain.ValueObjects;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.AddPet;

public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
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
        RuleFor(p => p.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(p => p.VolunteerId).NotEmpty().WithError(Error.NullValue);
    }
}