using CSharpFunctionalExtensions;
using HomeForPets.Api.Response;
using HomeForPets.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.Api.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this UnitResult<Error> result)
    {
        if (result.IsSuccess)
        {
            return new OkResult();
        }
        var statusCode = result.Error.ErrorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
        var envelope = Envelope.Error(result.Error);
        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    } 
    public static ActionResult<Envelope> ToResponse(this Error result)
    {
        var statusCode = result.ErrorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
        var envelope = Envelope.Error(result);
        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    } 
}