using FluentValidation;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Dtos.Volunteers;
using HomeForPets.Application.Validation;
using HomeForPets.Domain.VolunteersManagement.ValueObjects;

namespace HomeForPets.Application.VolunteersManagement.Commands.UpdateSocialNetworks;

public class UpdateSocialNetworkCommandValidator : AbstractValidator<UpdateSocialNetworkCommand>
{
    public UpdateSocialNetworkCommandValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleForEach(u => u.SocialNetworks).SetValidator(new UpdateSocialNetworksDtoValidator());
    }
}
public class UpdateSocialNetworksDtoValidator : AbstractValidator<SocialNetworkDto>
{
    public UpdateSocialNetworksDtoValidator()
    {
        RuleFor(u => u)
            .MustBeValueObject(f => 
                SocialNetwork.Create(f.Name, f.Path));
    }
}