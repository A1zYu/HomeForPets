using FluentValidation;
using HomeForPets.Api.Extensions;
using HomeForPets.Api.Response;
using HomeForPets.Application.Volunteers.CreateVolunteer;
using HomeForPets.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Envelope>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        [FromServices] IValidator<CreateVolunteerRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var validatorResult =await validator.ValidateAsync(request,cancellationToken);
        if (!validatorResult.IsValid)
        {
            return BadRequest(validatorResult.Errors);
        }
        var result = await handler.Handle(request, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return CreatedAtAction("", result.Value);
    }
}