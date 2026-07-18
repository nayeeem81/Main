using Main.Common;
using Main.Infrastructure;

namespace Main.WebAppCore.DependentServices;

public class TenantSetter: ITenantSetter
{
    public Guid CurrentTenantId
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