using Main.Common.Enums;

using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class Tenant
{
    //  for seed only
    public Tenant ( string key )
    {
        TenantId = key;
    }

    public Tenant ( )
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
    public EnumShopType ShopType
    {
        get; set;
    }

}
