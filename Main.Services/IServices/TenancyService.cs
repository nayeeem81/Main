using DataTransferModel;

using Domain.Model;
using Main.IRepository;

namespace Main.Services.IServices;

public class TenancyService: ITenancyService
{
    public readonly ITenantRepository _tenantRepository;

    public TenancyService (ITenantRepository tenantRepository)
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

    public async Task<TenantDisplayDataModel?> FindTenantAsync (string? hostName)
    {
        await _tenantRepository.FindCurrentTenantAsync (hostName);

        Tenant? tenant = _tenantRepository.CurrentTenant;

        if ( tenant == null )
        {
            CurrentTenant = null;
            TenancyFound = false;

            return null;
        }
        else
        {
            CurrentTenant =
                new TenantDisplayDataModel (
                    tenant.MyTenantId,
                    tenant.Name,
                    tenant.HostName,
                    tenant.Store,
                    tenant.SecretKey);

            TenancyFound = true;
        }

        return CurrentTenant;
    }
}
