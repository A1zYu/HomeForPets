using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace HomeForPets.Infrastructure.Authorization;

public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    public override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (string.IsNullOrWhiteSpace(policyName))
        {
            return Task.FromResult<AuthorizationPolicy?>(null);
        }
        
        var policy =  new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionAttribute(policyName))
            .Build();
        
        return Task.FromResult<AuthorizationPolicy?>(policy);
    }
}