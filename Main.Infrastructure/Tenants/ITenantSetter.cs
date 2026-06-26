using Main.Common;

namespace Main.Infrastructure;

public interface ITenantSetter
{
    string CurrentTenantId
    {
        get; set;
    }

    EnumStoreType TenantStore
    {
        get; set;
    }

    string TenantName
    {
        get; set;
    }
}
