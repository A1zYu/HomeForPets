using HomeForPets.Domain.Species;
using HomeForPets.Domain.VolunteersManagement;
using HomeForPets.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Infrastructure.DbContexts;

public class WriteDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Species> Species => Set<Species>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constants.DATABASE));
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory);
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSnakeCaseNamingConvention();

        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly,
            type=>type.FullName?.Contains("Configurations.Write") ?? false);
    }

    private ILoggerFactory CreateLoggerFactory =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}