using Microsoft.AspNetCore.Identity;
namespace Domain.Model;

public class ApplicationUser: IdentityUser
{
    public ApplicationUser ()
    {
    }

    public virtual ICollection<TenantUser> TenantUsers
    {
        get; set;
    } = new HashSet<TenantUser> ();
}
