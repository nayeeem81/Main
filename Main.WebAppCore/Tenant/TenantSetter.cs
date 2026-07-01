using Main.Common;
using Main.Infrastructure;

namespace Main.WebAppCore.Tenant;

public class TenantSetter: ITenantSetter
{
    public string CurrentTenantId
    {
        get; set;
    }

    public StoreType TenantStore
    {
        get; set;
    }

    public string TenantName
    {
        get; set;
    }
}