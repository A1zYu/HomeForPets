using HomeForPets.Core;
using HomeForPets.Core.Abstactions;
using HomeForPets.Core.Options;
using HomeForPets.Volunteers.Application;
using HomeForPets.Volunteers.Application.VolunteersManagement;
using HomeForPets.Volunteers.Infrastucture.DbContexts;
using HomeForPets.Volunteers.Infrastucture.Providers;
using HomeForPets.Volunteers.Infrastucture.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace HomeForPets.Volunteers.Infrastucture;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteerInfrastructure(
        this IServiceCollection service,
        IConfiguration configuration)
    {
        service
            .AddDatabaseContext()
            .AddRepositories()
            .AddDbContexts()
            .AddMinio(configuration);

        return service;
    }

    private static IServiceCollection AddDatabaseContext(this IServiceCollection service)
    {
        service.AddScoped<IUnitOfWork, UnitOfWork>();

        return service;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection service)
    {
        service.AddScoped<VolunteerWriteDbContext>();
        service.AddScoped<IReadDbContext, VolunteerReadDbContext>();

        return service;
    }


    private static IServiceCollection AddRepositories(this IServiceCollection service)
    {
        service.AddScoped<IVolunteersRepository, VolunteersRepository>();
        return service;
    }

    private static IServiceCollection AddMinio(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<MinioOptions>(configuration.GetSection(MinioOptions.MINIO));
        service.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                               ?? throw new ApplicationException("Missing minio configuration");

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.WithSsl);
        });
        service.AddScoped<IFileProvider, MinioProvider>();
        
        return service;
    }
}