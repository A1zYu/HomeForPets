using FluentValidation;
using HomeForPets.Application.Abstaction;
using HomeForPets.Application.File.Create;
using HomeForPets.Application.File.Delete;
using HomeForPets.Application.File.Get;
using HomeForPets.Application.VolunteersManagement.Commands.AddPet;
using HomeForPets.Application.VolunteersManagement.Commands.CreateVolunteer;
using HomeForPets.Application.VolunteersManagement.Commands.Delete;
using HomeForPets.Application.VolunteersManagement.Commands.Update;
using HomeForPets.Application.VolunteersManagement.Commands.UpdatePaymentDetails;
using HomeForPets.Application.VolunteersManagement.Commands.UpdateSocialNetworks;
using HomeForPets.Application.VolunteersManagement.Commands.UploadFilesToPet;
using HomeForPets.Application.VolunteersManagement.Queries;
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