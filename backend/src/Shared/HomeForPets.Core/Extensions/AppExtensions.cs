using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HomeForPets.Core.Extensions;

public static class AppExtensions
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        // await using var scope = app.Services.CreateAsyncScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        // await dbContext.Database.MigrateAsync();
    }
}