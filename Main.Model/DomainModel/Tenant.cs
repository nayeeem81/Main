using Main.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class Tenant: BaseEntity
{
    // seed
    public Tenant (Guid id)
    {
        TenantId = id;
        MyTenantId = id;
    }

    public Tenant ()
    {
    }

    [Key]
    public Guid TenantId
    {
        get; set;
    }

    [Required]
    public string Name
    {
        get; set;
    }

    [Required]
    public HostType TenantHostType
    {
        get; set;
    }

    [Required]
    public string HostName
    {
        get; set;
    }

    [Required]
    public StoreType Store
    {
        get; set;
    }

    public int? SmtpId
    {
        get; set;
    }


    public virtual EmailSmtp? EmaiSmtp
    {
        get; set;
    }

    public string? SecretKey
    {
        get; set;
    }

}
