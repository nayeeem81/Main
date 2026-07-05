using Main.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class Tenant
{
    // seed
    public Tenant (string key)
    {
        TenantId = key;
    }

    public Tenant ()
    {
    }

    [Key]
    public string TenantId
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

    public string TenantKey
    {
        get; set;
    }

}
