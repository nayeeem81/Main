using Main.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class Tenant: BaseEntity
{
    // seed
    public Tenant (Guid id,Guid myTenantId)
    {
        TenantId = id;
        MyTenantId = myTenantId;

        ModifiedBy = "e02fd0e4-00fd-000a-ca30-0F00a0898ba1";
        CreatedBy = "e02fd0e4-00fd-000a-ca30-0F00a0898ba1";
        CreatedDate = DateTime.MinValue;
        ModifiedDate = DateTime.MinValue;

        TenantCountry = Country.Bangladesh;
        IsActive = true;

        SessionUserId = "e02fd0e4-00fd-000a-ca30-0F00a0898ba1";
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
