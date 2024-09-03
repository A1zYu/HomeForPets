﻿using FluentValidation;
using HomeForPets.Application.Volunteers.CreateVolunteer;
using HomeForPets.Application.Volunteers.Delete;
using HomeForPets.Application.Volunteers.Update;
using HomeForPets.Application.Volunteers.UpdatePaymentDetails;
using HomeForPets.Application.Volunteers.UpdateSocialNetworks;
using Microsoft.Extensions.DependencyInjection;

namespace HomeForPets.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<CreateVolunteerHandler>();
        service.AddScoped<UpdateVolunteerHandler>();
        service.AddScoped<DeleteVolunteerHandler>();
        service.AddScoped<UpdateSocialNetworkHandler>();
        service.AddScoped<UpdatePaymentDetailsHandler>();
        service.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        return service;
    }
}