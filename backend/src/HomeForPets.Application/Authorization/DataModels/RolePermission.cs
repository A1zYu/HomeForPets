﻿namespace HomeForPets.Application.Authorization.DataModels;

public class RolePermission
{
    public Guid Id { get; set; }
    
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    
    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; }
}