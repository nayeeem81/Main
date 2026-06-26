using Microsoft.AspNetCore.Authorization;

namespace Main.WebAppCore.Tenant;

public class TenantRoleRequirement: IAuthorizationRequirement
{
    public string AllowedRole
    {
        get;
    }
    public TenantRoleRequirement (string allowedRole) => AllowedRole = allowedRole;
}
