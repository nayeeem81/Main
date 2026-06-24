using DataTransferModel;

namespace Main.Services;

public interface ITenancyService
{
    bool TenancyFound
    {
        get; set;
    }

    TenantDisplayDataModel? CurrentTenant
    {
        get; set;
    }

    Task FindTenantAsync ( string? hostName );
}
