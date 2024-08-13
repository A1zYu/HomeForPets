using HomeForPets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HomeForPets;

public class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    private const string DATABASE = "localDb";
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<PetPhoto> PetPhotos => Set<PetPhoto>();
    public DbSet<PaymentDetails> PaymentDetails => Set<PaymentDetails>();
    public DbSet<SocialNetwork> SocialNetworks => Set<SocialNetwork>();

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