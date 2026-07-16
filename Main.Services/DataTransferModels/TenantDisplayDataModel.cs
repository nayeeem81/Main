using Main.Common;

namespace DataTransferModel;

public class TenantDisplayDataModel
{
    public TenantDisplayDataModel ()
    {
    }

    public TenantDisplayDataModel
    (Guid tenantId,string name,string domain,StoreType shopType,string? key)
    {
        MyTenantId = tenantId;
        Name = name;
        Domain = domain;
        StoreType = shopType;
        SecretKey = key;
    }

    public Guid MyTenantId
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

    public string? SecretKey
    {
        get; set;
    }
}
