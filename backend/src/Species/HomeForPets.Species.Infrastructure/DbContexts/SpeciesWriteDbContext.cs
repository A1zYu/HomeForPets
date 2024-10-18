using HomeForPets.Species.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Species.Infrastructure.DbContexts;

public class SpeciesWriteDbContext(IConfiguration configuration) : DbContext
{
    private const string DATABASE = "localDb";
    public DbSet<Domain.Species> Species => Set<Domain.Species>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory);
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSnakeCaseNamingConvention();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpeciesWriteDbContext).Assembly,
            type=>type.FullName?.Contains("Configurations.Write") ?? false);
        modelBuilder.HasDefaultSchema("species");
    }
    private ILoggerFactory CreateLoggerFactory =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}