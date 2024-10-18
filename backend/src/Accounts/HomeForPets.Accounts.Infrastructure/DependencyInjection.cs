using System.Text;
using HomeForPets.Accounts.Application;
using HomeForPets.Accounts.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HomeForPets.Accounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsInfrastructure(
        this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddTransient<ITokenProvider, JwtTokenProvider>();
        
        service.Configure<JwtOptions>(configuration.GetSection(JwtOptions.JWT));
        
        service.RegisterIdentity();
        
        service.AddScoped<AuthorizationsDbContext>();
        
        return service;
    }

    private static void RegisterIdentity(this IServiceCollection service)
    {
        service
            .AddIdentity<User, Role>(option => { option.User.RequireUniqueEmail = true; })
            .AddEntityFrameworkStores<AuthorizationsDbContext>()
            .AddDefaultTokenProviders();
    }
}