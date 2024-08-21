using FluentValidation.Results;
using HomeForPets.Api.Response;
using HomeForPets.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.Api.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
    {
        var statusCode = error.ErrorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        var responseError = new ResponseError(error.Code, error.Message, null);

        var envelope = Envelope.Error([responseError]);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }

    public static ActionResult ToValidationResponse(this ValidationResult result)
    {
        if (result.IsValid)
        {
            throw new InvalidOperationException("Result can not be succeed");
        }
        var invalidErrors = result.Errors;
        var responsesError = (from invalidError in invalidErrors 
            let error = Error.Deserialize(invalidError.ErrorMessage) 
            select new ResponseError(error.Code, error.Message, invalidError.PropertyName));

        var envelope = Envelope.Error(responsesError);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}