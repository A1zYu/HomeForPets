using Microsoft.AspNetCore.Identity;

namespace HomeForPets.Application.Authorization.DataModels;

public class Role : IdentityRole<Guid>
{
    public List<RolePermission> RolePermissions { get; set; }
}