using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HomeForPets.Core.Extensions;

public static class AppExtensions
{
    public static async Task ApplyMigrations<T>(this WebApplication app) where T: DbContext
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<T>();
        await dbContext.Database.MigrateAsync();
    }
}