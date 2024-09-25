using FluentValidation;
using HomeForPets.Api.Controllers.Volunteers.Request;
using HomeForPets.Api.Extensions;
using HomeForPets.Api.Processor;
using HomeForPets.Api.Response;
using HomeForPets.Application.VolunteersManagement.Commands.AddPet;
using HomeForPets.Application.VolunteersManagement.Commands.CreateVolunteer;
using HomeForPets.Application.VolunteersManagement.Commands.Delete;
using HomeForPets.Application.VolunteersManagement.Commands.Update;
using HomeForPets.Application.VolunteersManagement.Commands.UpdatePaymentDetails;
using HomeForPets.Application.VolunteersManagement.Commands.UpdateSocialNetworks;
using HomeForPets.Application.VolunteersManagement.Commands.UploadFilesToPet;
using HomeForPets.Application.VolunteersManagement.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.Api.Controllers.Volunteers;

public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<IActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateMainInfoRequest request,
        [FromServices] UpdateVolunteerHandler handler,
        CancellationToken ct)
    {
        var result = await handler.Handle(request.ToCommand(id), ct);

        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerHandler handler,
        CancellationToken ct)
    {
        var request = new DeleteVolunteerCommand(id);
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
        [FromBody] UpdateSocialNetworksRequest request,
        [FromServices] UpdateSocialNetworkHandler handler,
        CancellationToken ct)
    {
        var result = await handler.Handle(request.ToCommand(id), ct);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpPut("{id:guid}/payment-details")]
    public async Task<IActionResult> UpdatePaymentDetails(
        [FromRoute] Guid id,
        [FromBody] UpdatePaymentDetailsRequest request,
        [FromServices] UpdatePaymentDetailsHandler handler,
        CancellationToken ct)
    {
        var result = await handler.Handle(request.ToCommand(id), ct);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult> AddPet(
        [FromRoute] Guid id,
        [FromBody] AddPetRequest request,
        [FromServices] AddPetHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpPost("{id:guid}/pet/{petId:guid}/files")]
    public async Task<IActionResult> AddPetPhotos(
        [FromRoute] Guid id,
        [FromRoute] Guid petId,
        [FromForm] IFormFileCollection files,
        [FromServices] UploadFilesToPetPhotoHandler handler,
        CancellationToken cancellationToken)
    {
        await using var fileProcessor = new FormFileProcessor();

        var filesDto = fileProcessor.Process(files);

        var command = new UploadFilesToPetPhotoCommand(id, petId, filesDto);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
        {
            result.Error.ToResponse();
        }

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] GetVolunteersWithPaginationRequest request,
        [FromServices] GetVolunteersWithPaginationHandler handler,
        CancellationToken cancellationToken = default)

    {
        var result = await handler.Handle(request.ToQuery(), cancellationToken);
        
        return Ok(result);
    }
}