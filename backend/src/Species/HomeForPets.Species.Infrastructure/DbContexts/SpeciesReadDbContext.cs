using HomeForPets.Core.Dtos.SpeciesDto;
using HomeForPets.Core.Dtos.Volunteers;
using HomeForPets.Species.Application;
using HomeForPets.Species.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Species.Infrastructure.DbContexts;

public class SpeciesReadDbContext(IConfiguration configuration) :DbContext, ISpeciesReadDbContext
{
    private const string DATABASE = "localDb";

    public IQueryable<BreedDto> Breeds => Set<BreedDto>();
    public IQueryable<SpeciesDto> Species => Set<SpeciesDto>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory);
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSnakeCaseNamingConvention();
        
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpeciesReadDbContext).Assembly,
            type=>type.FullName?.Contains("Configurations.Read") ?? false);
    }

    private ILoggerFactory CreateLoggerFactory =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
    
}