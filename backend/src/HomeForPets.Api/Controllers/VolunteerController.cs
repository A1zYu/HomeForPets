using HomeForPets.Api.Extensions;
using HomeForPets.Application.Volunteers.CreateVolunteer;
using Microsoft.AspNetCore.Mvc;

namespace HomeForPets.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request, cancellationToken);

        return result.ToResponse();
    }
}