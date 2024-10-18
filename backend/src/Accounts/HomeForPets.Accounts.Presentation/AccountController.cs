using HomeForPets.Accounts.Application.Commands.Login;
using HomeForPets.Accounts.Application.Commands.RegisterUser;
using HomeForPets.Accounts.Contacts.Interfaces.Request;
using HomeForPets.Framework;
using HomeForPets.Framework.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.Accounts.Presentation;

public class AccountController : ApplicationController
{
    [HttpPost("registration")]
    public async Task<IActionResult> Register(
     [FromBody] RegisterAccountRequest request,
     [FromServices] RegisterAccountHandler handler,
        CancellationToken cancellationToken=default)
    {
        var result = await handler.Handle(request.ToCommand(),cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.IsSuccess);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(
     [FromBody] LoginUserRequest request,
     [FromServices] LoginHandler handler,
        CancellationToken cancellationToken=default)
    {
        var result = await handler.Handle(request.ToCommand(),cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.Value);
    }

    [Permission("test.command")]
    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        return Ok();
    }
}