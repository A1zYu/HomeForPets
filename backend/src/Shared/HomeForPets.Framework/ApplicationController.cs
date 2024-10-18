using HomeForPets.Core;
using HomeForPets.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.Framework;
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