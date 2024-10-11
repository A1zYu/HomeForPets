using HomeForPets.Core.Abstaction;

namespace HomeForPets.Accounts.Application.Commands.RegisterUser;

public record RegisterAccountCommand(string Email, string Password, string UserName) : ICommand;