using Main.Common;

namespace DataTransferModel;

public class TenantDisplayDataModel
{
    public TenantDisplayDataModel ()
    {
    }

    public TenantDisplayDataModel (string tenantId,string name,string domain,EnumStoreType shopType)
    {
        TenantId = tenantId;
        Name = name;
        Domain = domain;
        StoreType = shopType;
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

    public EnumStoreType StoreType
    {
        get; set;
    }
}
