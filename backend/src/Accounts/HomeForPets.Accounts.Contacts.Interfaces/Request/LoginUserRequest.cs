
using HomeForPets.Accounts.Application.Commands.Login;

namespace HomeForPets.Accounts.Contacts.Interfaces.Request;

public record LoginUserRequest(string Email, string Password)
{
    public LoginCommand ToCommand() => new(Email, Password);
};