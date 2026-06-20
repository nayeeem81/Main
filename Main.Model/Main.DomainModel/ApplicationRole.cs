using Domain.Model;

using Microsoft.AspNetCore.Identity;

public class ApplicationRole: IdentityRole, IMustHaveTenant
{
    public string TenantId
    {
        get; set;
    }

    public ApplicationRole ( ) : base ( ) { }

    public ApplicationRole ( string roleName ) : base ( roleName ) { }
}
