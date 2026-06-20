using Main.Common.Enums;

namespace DataTransferModel;

public class TenantDisplayDataModel
{
    public TenantDisplayDataModel ( )
    {
    }

    public TenantDisplayDataModel ( string tenantId,string name,string domain,EnumShopType shopType )
    {
        TenantId = tenantId;
        Name = name;
        Domain = domain;
        ShopType = shopType;
    }

    public string TenantId
    {
        get; set;
    }

    public string Name
    {
        get; set;
    }

    public string Domain
    {
        get; set;
    }

    public EnumShopType ShopType
    {
        get; set;
    }
}
