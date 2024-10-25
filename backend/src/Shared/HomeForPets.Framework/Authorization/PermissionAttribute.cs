using Microsoft.AspNetCore.Authorization;

namespace HomeForPets.Framework.Authorization;

public class PermissionAttribute : AuthorizeAttribute, IAuthorizationRequirement
{
    public string Code { get; }

    public PermissionAttribute(string code) : base(policy:code)
    {
        Code = code;
    }
}