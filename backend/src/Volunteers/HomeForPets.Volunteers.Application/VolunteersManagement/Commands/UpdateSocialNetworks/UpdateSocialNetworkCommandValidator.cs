using FluentValidation;
using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.Core.Validation;
using HomeForPets.Volunteers.Domain.ValueObjects;

namespace HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UpdateSocialNetworks;

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