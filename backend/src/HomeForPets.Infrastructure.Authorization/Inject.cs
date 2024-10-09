using System.Text;
using HomeForPets.Application.Authorization;
using HomeForPets.Application.Authorization.DataModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Minio;
using FileInfo = HomeForPets.Application.Files.FileInfo;

namespace HomeForPets.Infrastructure.Authorization;

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
                    ValidateIssuerSigningKey = true
                };
            });

        service.AddScoped<AuthorizationsDbContext>();
        
        service.AddAuthorization();

        service.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        service.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        return service;
    }
}