using FluentValidation;
using HomeForPets.Application.Validation;
using HomeForPets.Domain.Volunteers;

namespace HomeForPets.Application.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworkRequestValidator : AbstractValidator<UpdateSocialNetworkRequest>
{
    public UpdateSocialNetworkRequestValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
    }
}
public class UpdateSocialNetworksDtoValidator : AbstractValidator<UpdateSocialNetworksDto>
{
    public UpdateSocialNetworksDtoValidator()
    {
        RuleForEach(u => u.SocialNetworks)
            .MustBeValueObject(f => 
                SocialNetwork.Create(f.Name, f.Path));
    }
}