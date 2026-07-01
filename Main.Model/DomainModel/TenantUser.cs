namespace Domain.Model;

public class TenantUser
{
    public TenantUser ()
    {
    }

    public string UserId { get; set; } = string.Empty;

    public virtual ApplicationUser User { get; set; } = null!;

    public string TenantId { get; set; } = string.Empty;

    public virtual Tenant Tenant { get; set; } = null!;

    public string TenantRole { get; set; } = string.Empty;
}
