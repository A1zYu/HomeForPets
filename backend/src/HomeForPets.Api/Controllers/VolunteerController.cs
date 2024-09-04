using FluentValidation;
using HomeForPets.Api.Extensions;
using HomeForPets.Api.Response;
using HomeForPets.Application.Volunteers.CreateVolunteer;
using HomeForPets.Application.Volunteers.Delete;
using HomeForPets.Application.Volunteers.Update;
using HomeForPets.Application.Volunteers.UpdatePaymentDetails;
using HomeForPets.Application.Volunteers.UpdateSocialNetworks;
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

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<IActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateMainInfoDto dto,
        [FromServices] UpdateVolunteerHandler handler,
        [FromServices] IValidator<UpdateMainInfoRequest> validator,
        CancellationToken ct)
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

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerHandler handler,
        [FromServices] IValidator<DeleteVolunteerRequest> validator,
        CancellationToken ct)
    {
        var request = new DeleteVolunteerRequest(id);

        var validationResult = await validator.ValidateAsync(request, ct);

        if (validationResult.IsValid == false)
            return validationResult.ToValidationResponse();

        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
        {
            result.Error.ToResponse();
        }

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<IActionResult> UpdateSocialNetworks(
        [FromRoute] Guid id,
        [FromBody] UpdateSocialNetworksDto dto,
        [FromServices] UpdateSocialNetworkHandler handler,
        [FromServices] IValidator<UpdateSocialNetworkRequest> validator,
        CancellationToken ct)
    {
        var request = new UpdateSocialNetworkRequest(id, dto);

        var validatorResult = await validator.ValidateAsync(request, ct);
        if (validatorResult.IsValid == false)
        {
            return validatorResult.ToValidationResponse();
        }

        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
        {
            result.Error.ToResponse();
        }

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpPut("{id:guid}/payment-details")]
    public async Task<IActionResult> UpdatePaymentDetails(
        [FromRoute] Guid id,
        [FromBody] UpdatePaymentDetailsDto dto,
        [FromServices] UpdatePaymentDetailsHandler handler,
        [FromServices] IValidator<UpdatePaymentDetailsRequest> validator,
        CancellationToken ct)
    {
        var request = new UpdatePaymentDetailsRequest(id, dto);

        var validatorResult = await validator.ValidateAsync(request, ct);
        if (validatorResult.IsValid == false)
        {
            return validatorResult.ToValidationResponse();
        }

        var result = await handler.Handle(request, ct);
        if (result.IsFailure)
        {
            result.Error.ToResponse();
        }

        return Ok(Envelope.Ok(result.Value));
    }
}