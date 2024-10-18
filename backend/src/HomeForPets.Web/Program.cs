using System.Text;
using HomeForPets.Accounts.Application;
using HomeForPets.Accounts.Infrastructure;
using HomeForPets.Web.Middlewares;
using HomeForPets.Core.Extensions;
using HomeForPets.Framework.Authorization;
using HomeForPets.Framework.Extensions;
using HomeForPets.Species.Application;
using HomeForPets.Species.Infrastructure;
using HomeForPets.Species.Infrastructure.DbContexts;
using HomeForPets.Volunteers.Application;
using HomeForPets.Volunteers.Infrastucture;
using HomeForPets.Volunteers.Infrastucture.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")?? 
                 throw new ArgumentNullException("Seq"))
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

builder.Services.AddSwaggerGenWithAuth();

builder.Services.AddSerilog();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtOptions = builder.Configuration.GetSection(JwtOptions.JWT).Get<JwtOptions>()
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

builder.Services.AddAuthorization();

builder.Services
    .AddAccountsApplication()
    .AddAccountsInfrastructure(builder.Configuration)
    .AddSpeciesApplication()
    .AddSpeciesInfrastructure(builder.Configuration)
    .AddVolunteersApplication()
    .AddVolunteerInfrastructure(builder.Configuration);

builder.Services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseExceptionMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.ApplyMigrations<SpeciesWriteDbContext>();
    await app.ApplyMigrations<VolunteerWriteDbContext>();
    await app.ApplyMigrations<AuthorizationsDbContext>();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();