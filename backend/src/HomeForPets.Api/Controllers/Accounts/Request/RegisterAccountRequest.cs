using HomeForPets.Application.Authorization.Commands.RegisterUser;

namespace HomeForPets.Api.Controllers.Accounts.Request;

public record RegisterAccountRequest(string Email, string Password, string UserName)
{
    public RegisterAccountCommand ToCommand() => new(Email, Password, UserName);
}