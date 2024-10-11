using HomeForPets.Accounts.Application.Commands.RegisterUser;

namespace HomeForPets.Accounts.Contacts.Interfaces.Request;

public record RegisterAccountRequest(string Email, string Password, string UserName)
{
    public RegisterAccountCommand ToCommand() => new(Email, Password, UserName);
}