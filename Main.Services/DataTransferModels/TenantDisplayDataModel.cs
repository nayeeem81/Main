using Main.Common;

namespace DataTransferModel;

public class TenantDisplayDataModel
{
    public TenantDisplayDataModel ()
    {
    }

    public TenantDisplayDataModel
    (string tenantId,string name,string domain,StoreType shopType,string key)
    {
        TenantId = tenantId;
        Name = name;
        Domain = domain;
        StoreType = shopType;
        TokenKey = key;
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

    public StoreType StoreType
    {
        get; set;
    }

    public string? TokenKey
    {
        get; set;
    }
}
