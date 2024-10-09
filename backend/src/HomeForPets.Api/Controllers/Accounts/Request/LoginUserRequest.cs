using HomeForPets.Application.Authorization.Commands.Login;

namespace HomeForPets.Api.Controllers.Accounts.Request;

public record LoginUserRequest(string Email, string Password)
{
    public LoginCommand ToCommand() => new(Email, Password);
};