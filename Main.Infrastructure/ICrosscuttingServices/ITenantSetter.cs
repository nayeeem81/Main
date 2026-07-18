using Main.Common;

namespace Main.Infrastructure.CrosscuttingHelperServices;

public interface ITenantSetter
{
    Guid CurrentTenantId
    {
        get; set;
    }

    StoreType TenantStore
    {
        get; set;
    }

    string TenantName
    {
        get; set;
    }
}
