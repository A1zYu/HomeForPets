﻿using FluentValidation;
using HomeForPets.Application.Volunteers.CreateVolunteer;
using Microsoft.Extensions.DependencyInjection;

namespace HomeForPets.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<CreateVolunteerHandler>();
        service.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        return service;
    }
}