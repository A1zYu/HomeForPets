using Microsoft.AspNetCore.Authorization;

namespace HomeForPets.Infrastructure.Authorization;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionAttribute attribute)
    {
        var permission  = context.User.Claims.FirstOrDefault(c => c.Type == "Permission");
        if (permission is null)
        {
            return;
        }

        if (permission.Value == attribute.Code)
        {
            context.Succeed(attribute);
        }
    }
}