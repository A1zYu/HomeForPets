using CSharpFunctionalExtensions;
using HomeForPets.Application.Abstaction;
using HomeForPets.Application.Authorization.DataModels;
using HomeForPets.Domain.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Application.Authorization.Commands.RegisterUser;

public class RegisterAccountHandler : ICommandHandler<RegisterAccountCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RegisterAccountHandler> _logger;

    public RegisterAccountHandler(UserManager<User> userManager, ILogger<RegisterAccountHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(RegisterAccountCommand command, CancellationToken ct)
    {
        var existingUser =await _userManager.FindByEmailAsync(command.Email);
        if (existingUser is not null)
        {
            return Errors.General.AlreadyExist().ToErrorList();
        }

        var user = new User()
        {
            Email = command.Email,
            UserName = command.UserName,
        };
        var result = await _userManager.CreateAsync(user, command.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors
                .Select(x=>Error.Failure(x.Code,x.Description))
                .ToList();
            return new ErrorList(errors);
        }

        _logger.LogInformation("User :{user} created a new account with password",command.Email);
        
        return UnitResult.Success<ErrorList>();
    }
}