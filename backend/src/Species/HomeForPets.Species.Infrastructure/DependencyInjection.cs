using HomeForPets.Core.Abstactions;
using HomeForPets.Species.Application;
using HomeForPets.Species.Application.SpeciesManagement;
using HomeForPets.Species.Infrastructure.DbContexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeForPets.Species.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesInfrastructure(
        this IServiceCollection service,
        IConfiguration configuration)
    {
        service
            .AddDatabaseContext()
            .AddRepositories()
            .AddDbContexts();

        return service;
    }

    private static IServiceCollection AddDatabaseContext(this IServiceCollection service)
    {
        service.AddScoped<IUnitOfWork, SpeciesUnitOfWork>();

        return service;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection service)
    {
        service.AddScoped<SpeciesWriteDbContext>();
        service.AddScoped<ISpeciesReadDbContext, SpeciesReadDbContext>();

        return service;
    }


    private static IServiceCollection AddRepositories(this IServiceCollection service)
    {
        service.AddScoped<ISpeciesRepository, SpeciesRepository>();
        return service;
    }
    
}