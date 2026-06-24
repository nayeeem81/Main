using Domain.Model;

namespace IRepository;

public interface ITenantRepository
{
    TenantInfo? CurrentTenant
    {
        get; set;
    }

    Task FindCurrentTenantAsync ( string? hostName );

}
