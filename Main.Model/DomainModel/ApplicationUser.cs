using Microsoft.AspNetCore.Identity;
namespace Domain.Model;

public class ApplicationUser: IdentityUser
{
    public ApplicationUser (string id)
    {
        Id = id;
    }

    public ApplicationUser ()
    {
    }

    public virtual ICollection<TenantUser> TenantUsers
    {
        get; set;

    } = new HashSet<TenantUser> ();
}
