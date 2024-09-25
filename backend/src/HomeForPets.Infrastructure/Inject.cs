using HomeForPets.Application.Database;
using HomeForPets.Application.Files;
using HomeForPets.Application.Messaging;
using HomeForPets.Application.VolunteersManagement;
using HomeForPets.Infrastructure.BackgroundServices;
using HomeForPets.Infrastructure.DbContexts;
using HomeForPets.Infrastructure.Files;
using HomeForPets.Infrastructure.MessageQueues;
using HomeForPets.Infrastructure.Options;
using HomeForPets.Infrastructure.Providers;
using HomeForPets.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using FileInfo = HomeForPets.Application.Files.FileInfo;

namespace HomeForPets.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection service,
        IConfiguration configuration)
    {
        service
            .AddDbContexts()
            .AddMinio(configuration)
            .AddRepositories()
            .AddDatabase()
            .AddHostedServices()
            .AddFilesCleanerServices()
            .AddMessageQueues();

        return service;
    }
    private static IServiceCollection AddMessageQueues(this IServiceCollection service)
    {
        service.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>,InMemoryMessageQueue<IEnumerable<FileInfo>>>();
        
        return service;
    }
    private static IServiceCollection AddFilesCleanerServices(this IServiceCollection service)
    {
        service.AddScoped<IFilesCleanerService, FilesCleanerService>();
        return service;
    }
    private static IServiceCollection AddHostedServices(this IServiceCollection service)
    {
        service.AddHostedService<FilesCleanerBackgroundService>();
        
        return service;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection service)
    {
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        
        return service;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection service)
    {
        service.AddScoped<IVolunteersRepository, VolunteersRepository>();
        
        return service;
    }
    private static IServiceCollection AddDbContexts(this IServiceCollection service)
    {
        service.AddScoped<WriteDbContext>();
        service.AddScoped<IReadDbContext, ReadDbContext>();
        
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