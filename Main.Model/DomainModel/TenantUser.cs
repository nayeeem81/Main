using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model;

public class TenantUser: BaseEntity
{
    public TenantUser (int id)
    {
        TenantUserId = id;
    }

    public TenantUser ()
    {
    }

    [Key]
    public int TenantUserId
    {
        get; set;
    }

    public string UserId { get; set; } = string.Empty;

    [ForeignKey ("UserId")]
    public virtual ApplicationUser User { get; set; } = null!;

    public string TenantRole { get; set; } = string.Empty;

}
