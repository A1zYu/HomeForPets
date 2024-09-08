﻿using FluentValidation;
using HomeForPets.Application.Dtos;
using HomeForPets.Application.Validation;
using HomeForPets.Domain.Volunteers;

namespace HomeForPets.Application.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworkCommandValidator : AbstractValidator<UpdateSocialNetworkCommand>
{
    public UpdateSocialNetworkCommandValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleFor(u => u.SocialNetworks).SetValidator(new UpdateSocialNetworksDtoValidator());
    }
}
public class UpdateSocialNetworksDtoValidator : AbstractValidator<IEnumerable<SocialNetworkDto>>
{
    public UpdateSocialNetworksDtoValidator()
    {
        RuleForEach(u => u)
            .MustBeValueObject(f => 
                SocialNetwork.Create(f.Name, f.Path));
    }
}