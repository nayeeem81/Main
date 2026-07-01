using Domain.Model;

namespace Main.IRepository;

public interface ITenantRepository
{
    Tenant? CurrentTenant
    {
        get; set;
    }

    Task FindCurrentTenantAsync (string? hostName);

}
