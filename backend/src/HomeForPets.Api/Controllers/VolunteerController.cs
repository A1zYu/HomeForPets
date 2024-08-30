using FluentValidation;
using HomeForPets.Api.Extensions;
using HomeForPets.Api.Response;
using HomeForPets.Application.Volunteers.CreateVolunteer;
using HomeForPets.Application.Volunteers.Update;
using HomeForPets.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.Api.Controllers;

public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<IActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateMainInfoDto dto,
        [FromServices] UpdateVolunteerHandler handler,
        [FromServices] IValidator<UpdateMainInfoRequest> validator,
        CancellationToken ct
        )
    {
        var request = new UpdateMainInfoRequest(id, dto);
        var validationResult = await validator.ValidateAsync(request, ct);
        if (validationResult.IsValid == false)
            return validationResult.ToValidationResponse();
        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
        {
            result.Error.ToResponse();
        }
        return Ok(result.Value);
    }
}