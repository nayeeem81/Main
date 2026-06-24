namespace Domain.Model;

public class UserTenant
{
    public UserTenant ( )
    {
    }

    public string UserId { get; set; } = string.Empty;

    public virtual ApplicationUser User { get; set; } = null!;

    public string TenantId { get; set; } = string.Empty;

    public virtual TenantInfo Tenant { get; set; } = null!;

    public string TenantRole { get; set; } = string.Empty;
}
