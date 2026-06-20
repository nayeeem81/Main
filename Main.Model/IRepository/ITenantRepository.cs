using Domain.Model;

namespace IRepository;

public interface ITenantRepository
{
    Tenant? CurrentTenant
    {
        get; set;
    }

    Task FindCurrentTenantAsync ( string? hostName );

}
