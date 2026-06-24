using Microsoft.AspNetCore.Identity;
namespace Domain.Model;

public class ApplicationUser: IdentityUser
{
    public ApplicationUser ( )
    {
    }

    public virtual ICollection<UserTenant> UserTenants
    {
        get; set;
    } = new HashSet<UserTenant> ( );
}
