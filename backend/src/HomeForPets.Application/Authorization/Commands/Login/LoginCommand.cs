using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.Authorization.Commands.Login;

public record LoginCommand(string Email,string Password):ICommand;