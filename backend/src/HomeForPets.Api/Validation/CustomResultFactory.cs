using HomeForPets.Api.Response;
using HomeForPets.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace HomeForPets.Api.Validation;

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context,
        ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails is null)
        {
            throw new ArgumentNullException("ValidationProblemDetails is null");
        }

        List<ResponseError> errors = [];   
        foreach (var (invalidField, validationErrors) in validationProblemDetails.Errors)
        {
            var responsesError = (from errorMessages in validationErrors 
                let error = Error.Deserialize(errorMessages) 
                select new ResponseError(error.Code, error.Message, invalidField));
            errors.AddRange(responsesError);            
        }
        var envelope = Envelope.Error(errors);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
    
}