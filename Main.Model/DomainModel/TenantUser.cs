using Main.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model;

public class TenantUser: BaseEntity
{
    public TenantUser (int id)
    {
        TenantUserId = id;
        ModifiedBy = "e02fd0e4-00fd-000a-ca30-0F00a0898ba1";
        CreatedBy = "e02fd0e4-00fd-000a-ca30-0F00a0898ba1";
        CreatedDate = DateTime.MinValue;
        ModifiedDate = DateTime.MinValue;

        TenantCountry = Country.Bangladesh;
        IsActive = true;

        SessionUserId = "e02fd0e4-00fd-000a-ca30-0F00a0898ba1";
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
