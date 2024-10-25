using HomeForPets.Framework;
using HomeForPets.Species.Application.SpeciesManagement.Commands.AddBreed;
using HomeForPets.Species.Application.SpeciesManagement.Commands.CreateSpecies;
using HomeForPets.Species.Application.SpeciesManagement.Commands.DeleteBreedToSpecies;
using HomeForPets.Species.Application.SpeciesManagement.Commands.DeleteSpecies;
using HomeForPets.Species.Application.SpeciesManagement.Queries.GetBreedsBySpecial;
using HomeForPets.Species.Application.SpeciesManagement.Queries.GetSpeciesWithPagination;
using HomeForPets.Species.Controllers.Request;
using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.Species.Controllers;

public class SpeciesController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> AddSpecies(
        [FromBody] CreateSpeciesRequest request,
        [FromServices] CreateSpeciesHandler handler,
        CancellationToken cancellationToken)
    {
        var result =await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return Ok(result.Value);
    }

    [HttpPost("{speciesId:guid}/breed")]
    public async Task<IActionResult> AddBreed(
        [FromRoute] Guid speciesId,
        [FromBody] AddBreedRequest request,
        [FromServices] AddBreedHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(speciesId), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.Value);
    }

    [HttpDelete("{speciesId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid speciesId,
        [FromServices] DeleteSpeciesHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteSpeciesCommand(speciesId);
        var result =await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        return Ok(result.IsSuccess);
    }

    [HttpDelete("{speciesId:guid}/breed")]
    public async Task<IActionResult> DeleteBreed(
        [FromRoute] Guid speciesId,
        [FromBody] DeleteBreedRequest request,
        [FromServices] DeleteBreedToSpeciesHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(speciesId), cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.IsSuccess);
    }

    [HttpGet("{speciesId:guid}/breeds")]
    public async Task<IActionResult> GetBreedsBySpecies(
        [FromRoute] Guid speciesId,
        [FromServices] GetBreedsBySpecialHandler handler,
        CancellationToken cancellationToken
    )
    {
        
        var query = new GetBreedsBySpecialQuery(speciesId);        
        var result = await handler.Handle(query, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.Value);
    }

    [HttpGet("specials")]
    public async Task<IActionResult> GetSpecials(
        [FromQuery] GetSpeciesWithPaginationRequest request,
        [FromServices] GetSpeciesWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        var result =await handler.Handle(request.ToQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }
        
        return Ok(result.Value);
    }
}