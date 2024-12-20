﻿using HomeForPets.Core.Processor;
using HomeForPets.Framework;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.AddPet;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.ChangePetStatus;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.CreateVolunteer;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.Delete;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.DeletePet;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.DeletePhotoPet;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.SetMainPetPhoto;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.Update;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UpdateMainInfoForPet;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UpdatePaymentDetails;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UpdateSocialNetworks;
using HomeForPets.Volunteers.Application.VolunteersManagement.Commands.UploadFilesToPet;
using HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetPetById;
using HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetPetsWithPagination;
using HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetVolunteerById;
using HomeForPets.Volunteers.Application.VolunteersManagement.Queries.GetVolunteersWithPagination;
using HomeForPets.Volunteers.Controllers.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.Volunteers.Controllers;

public class VolunteersController : ApplicationController
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

        return Ok(result.Value);
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

        return Ok(result.Value);
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

        return Ok(result.Value);
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

        return Ok(result.Value);
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

        return Ok(result.Value);
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

        return Ok(result.Value);
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

        return Ok(result.Value);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] GetVolunteersWithPaginationRequest request,
        [FromServices] GetVolunteersWithPaginationHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToQuery(), cancellationToken);

        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        [FromServices] GetVolunteerByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new GetVolunteerByIdQuery{ Id = id };
        var result = await handler.Handle(command, cancellationToken);
        return Ok(result.Value);
    }

    [HttpPut("{volunteerId:guid}/pet")]
    public async Task<IActionResult> UpdatePet(
        [FromRoute] Guid volunteerId,
        [FromBody] UpdateMainInfoPetRequst request,
        [FromServices] UpdateMainInfoForPetHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result =await handler.Handle(request.ToCommand(volunteerId),cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.Value);
    }

    [HttpDelete("{volunteerId:guid}/file-pet")]
    public async Task<IActionResult> DeletePhoto(
        [FromRoute] Guid volunteerId,
        [FromBody] DeleteFileForPetRequest request,
        [FromServices] DeletePetPhotoHandler photoHandler,
        CancellationToken cancellationToken = default)
    {
        var result = await photoHandler.Handle(request.ToCommand(volunteerId), cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.IsSuccess);
    }

    [HttpPut("{volunteerId:guid}/status-pet")]
    public async Task<IActionResult> ChangeStatusPet(
        [FromRoute] Guid volunteerId,
        [FromBody] ChangeStatusPetRequest request,
        [FromServices] PetChangeStatusHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(volunteerId), cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
  
        
        return Ok(result.IsSuccess);
    }

    [HttpDelete("{volunteerId:guid}/pet/{petId:guid}")]
    public async Task<IActionResult> DeletePetById(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] DeletePetHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeletePetCommand(volunteerId, petId);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.IsSuccess);
    }

    [HttpGet("pets")]
    public async Task<IActionResult> GetPets(
        [FromRoute] Guid volunteerId,
        [FromQuery] GetPetsWithPaginationRequest request,
        [FromServices] GetFilteredPetsWithPaginationHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result =await handler.Handle(request.ToQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.Value);
    }

    [HttpGet("pet/{petId:guid}")]
    public async Task<IActionResult> GetPetById(
        [FromRoute] Guid petId,
        [FromServices] GetPetByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPetByIdQuery(petId);
        var result = await handler.Handle(query, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        return Ok(result.Value);
    }

    [HttpPut("{volunteerId}/pet/{petId:guid}/main-photo/{photoId:guid}")]
    public async Task<IActionResult> SetMainPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromRoute] Guid photoId,
        [FromServices] SetMainPetPhotoHandler handler,
        CancellationToken cancellationToken = default
    )
    {
        var command = new SetMainPetPhotoCommand(volunteerId, petId, photoId);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.IsSuccess);
    }
}