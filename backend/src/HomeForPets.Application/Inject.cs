using FluentValidation;
using HomeForPets.Core.Abstaction;
using Microsoft.Extensions.DependencyInjection;

namespace HomeForPets.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service
            .AddCommands()
            .AddQueries()
            .AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return service;
    }

    private static IServiceCollection AddCommands(this IServiceCollection service)
    {
        service.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<>),typeof(ICommandHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
        );
        return service;
    }
    
    private static IServiceCollection AddQueries(this IServiceCollection service)
    {
        service.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes
                .AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
        );
        return service;
    }
    
}