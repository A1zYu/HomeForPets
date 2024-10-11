using HomeForPets.Core.Abstaction;

namespace HomeForPets.Accounts.Application.Commands.Login;

public record LoginCommand(string Email,string Password):ICommand;