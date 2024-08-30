using FluentValidation;
using HomeForPets.Application.Volunteers.CreateVolunteer;
using HomeForPets.Application.Volunteers.Update;
using Microsoft.Extensions.DependencyInjection;

namespace HomeForPets.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<CreateVolunteerHandler>();
        service.AddScoped<UpdateVolunteerHandler>();
        service.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        return service;
    }
}