using HomeForPets.Api.Response;
using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.Api.Controllers;
[ApiController]
[Route("[controller]")]
public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);
        return base.Ok(envelope);
    }
}