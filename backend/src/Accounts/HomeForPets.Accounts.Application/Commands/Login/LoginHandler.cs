using CSharpFunctionalExtensions;
using HomeForPets.Accounts.Domain;
using HomeForPets.Core;
using HomeForPets.Core.Abstactions;
using HomeForPets.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HomeForPets.Accounts.Application.Commands.Login;

public class LoginHandler : ICommandHandler<string,LoginCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<LoginHandler> _logger;

    public LoginHandler(
        UserManager<User> userManager,
        ITokenProvider tokenProvider,
        ILogger<LoginHandler> logger)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
        _logger = logger;
    }

    public async Task<Result<string, ErrorList>> Handle(
        LoginCommand command, CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            return Errors.User.InvalidCredentials().ToErrorList();
        }

        var passwordConfirmed = await _userManager.CheckPasswordAsync(user, command.Password);
        if (!passwordConfirmed)
        {
            return Errors.User.InvalidCredentials().ToErrorList();
        }

        var token =_tokenProvider.GenerateAccessToken(user,ct);

        _logger.LogInformation("Successfully logged in.");

        return token;
    }
}