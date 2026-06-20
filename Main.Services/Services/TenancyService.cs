using DataTransferModel;

using Domain.Model;

using IRepository;

namespace Main.Services;

public class TenancyService: ITenancyService
{
    public readonly ITenantRepository _tenantRepository;

    public TenancyService ( ITenantRepository tenantRepository )
    {
        TenantTd = "";
        TenancyFound = false;
        _tenantRepository = tenantRepository;
    }

    public string TenantTd
    {
        get; set;
    }

    public bool TenancyFound
    {
        get;
        set;
    }

    public TenantDisplayDataModel? CurrentTenant
    {
        get; set;
    }

    public async Task FindTenant ( string? hostName )
    {
        await _tenantRepository.FindCurrentTenant ( hostName );

        Tenant? tenant = _tenantRepository.CurrentTenant;

        if ( tenant == null )
        {
            CurrentTenant = null;
            TenancyFound = false;
        }
        else
        {
            CurrentTenant = new TenantDisplayDataModel ( tenant.TenantId,tenant.Name,
                tenant.Domain,tenant.ShopType );

            TenancyFound = true;
        }
    }
}
