using Microsoft.AspNetCore.Authorization;

namespace HomeForPets.Core.Authorization;

public class PermissionAttribute : AuthorizeAttribute, IAuthorizationRequirement
{
    public string Code { get; }

    public PermissionAttribute(string code) : base(policy:code)
    {
        Code = code;
    }
}