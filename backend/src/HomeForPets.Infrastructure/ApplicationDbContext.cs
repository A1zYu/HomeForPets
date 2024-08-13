using HomeForPets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HomeForPets;

public class ApplicationDbContext(IConfiguration configuration): DbContext
{
    private const string DATABASE= "localDb";
    public DbSet<Pet> Pets => Set<Pet>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
    }
}