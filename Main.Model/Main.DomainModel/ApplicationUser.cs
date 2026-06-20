using Microsoft.AspNetCore.Identity;

namespace Domain.Model;

public class ApplicationUser: IdentityUser, IMustHaveTenant
{
    public ApplicationUser ( )
    {
    }

    public string TenantId
    {
        get;
        set;
    }
}
