using HomeForPets.Application.Abstaction;

namespace HomeForPets.Application.Authorization.Commands.RegisterUser;

public record RegisterAccountCommand(string Email, string Password, string UserName) : ICommand;