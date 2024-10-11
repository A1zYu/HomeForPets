using System.Text;
using HomeForPets.Accounts.Application;
using HomeForPets.Accounts.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HomeForPets.Accounts.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddAuthorizationInfrastructure(
        this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddTransient<ITokenProvider, JwtTokenProvider>();
        
        service.Configure<JwtOptions>(configuration.GetSection(JwtOptions.JWT));
        
        service
            .AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AuthorizationsDbContext>();
        
        service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtOptions = configuration.GetSection(JwtOptions.JWT).Get<JwtOptions>()
                                 ?? throw new ApplicationException("Missing jwt configuration");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        service.AddScoped<AuthorizationsDbContext>();
        
        service.AddAuthorization();
        
        return service;
    }
}