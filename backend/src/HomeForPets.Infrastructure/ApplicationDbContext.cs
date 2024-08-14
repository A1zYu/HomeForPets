using HomeForPets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HomeForPets;

public class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    private const string DATABASE = "localDb";
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE))
            .UseLoggerFactory(CreateLoggerFactory)
            .UseSnakeCaseNamingConvention();

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    private ILoggerFactory CreateLoggerFactory => 
        LoggerFactory.Create(builder => { builder.AddConsole(); });
    
}