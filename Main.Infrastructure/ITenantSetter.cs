using Main.Common;

namespace Main.Infrastructure;

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
