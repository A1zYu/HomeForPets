using Microsoft.AspNetCore.Identity;

namespace HomeForPets.Accounts.Domain;

public class Role : IdentityRole<Guid>
{
    public List<RolePermission> RolePermissions { get; set; }
}