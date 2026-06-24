using DataTransferModel;

using Domain.Model;

using IRepository;

namespace Main.Services;

public class TenancyService: ITenancyService
{
    public readonly ITenantRepository _tenantRepository;

    public TenancyService ( ITenantRepository tenantRepository )
    {
        TenancyFound = false;
        _tenantRepository = tenantRepository;
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

    public async Task FindTenantAsync ( string? hostName )
    {
        await _tenantRepository.FindCurrentTenantAsync ( hostName );

        TenantInfo? tenant = _tenantRepository.CurrentTenant;

        if ( tenant == null )
        {
            CurrentTenant = null;
            TenancyFound = false;
        }
        else
        {
            CurrentTenant = new TenantDisplayDataModel ( tenant.TenantId,tenant.Name,
                tenant.Domain,tenant.Store );

            TenancyFound = true;
        }
    }
}
