using Main.Common.Enums;

using System.ComponentModel.DataAnnotations;
namespace Domain.Model;

public class TenantInfo
{
    // seed
    public TenantInfo ( string key )
    {
        TenantId = key;
    }

    public TenantInfo ( )
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
    public string Domain
    {
        get; set;
    }

    [Required]
    public EnumStoreType Store
    {
        get; set;
    }

}
