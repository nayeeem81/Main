using Main.Common;
using Main.Infrastructure.CrosscuttingHelperServices;

namespace Main.WebAppCore.Tenant;

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