using FluentValidation;
using HomeForPets.Core.Abstactions;
using Microsoft.Extensions.DependencyInjection;

namespace HomeForPets.Species.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesApplication(this IServiceCollection service)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        service.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<>),typeof(ICommandHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
        );
        
        service.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
        );
        
        service
            .AddValidatorsFromAssembly(assembly);
        
        return service;
    }
}