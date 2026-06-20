using Main.Common.Enums;

namespace Main.Infrastructure;

public interface ITenantSetter
{
    string CurrentTenantId
    {
        get; set;
    }

    EnumShopType TenantShopType
    {
        get; set;
    }

    string TenantName
    {
        get; set;
    }
}
