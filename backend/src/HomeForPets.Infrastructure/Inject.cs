using HomeForPets.Application.Volunteers;
using HomeForPets.Application.Volunteers.CreateVolunteer;
using HomeForPets.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HomeForPets.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service)
    {
        service.AddScoped<ApplicationDbContext>();
        service.AddScoped<IVolunteersRepository, VolunteersRepository>();
        return service;
    }
}