using HomeForPets.Api.Controllers.Accounts.Request;
using HomeForPets.Api.Extensions;
using HomeForPets.Application.Authorization.Commands.Login;
using HomeForPets.Application.Authorization.Commands.RegisterUser;
using HomeForPets.Infrastructure.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.Api.Controllers.Accounts;

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