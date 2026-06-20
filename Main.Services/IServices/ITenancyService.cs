using DataTransferModel;

namespace Main.Services;

public interface ITenancyService
{
    string TenantTd
    {
        get;
        set;
    }

    bool TenancyFound
    {
        get; set;
    }

    TenantDisplayDataModel? CurrentTenant
    {
        get; set;
    }

    Task FindTenant ( string? hostName );
}
