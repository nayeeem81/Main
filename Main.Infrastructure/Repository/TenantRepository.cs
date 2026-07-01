using Domain.Model;

using Main.IRepository;

using Main.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class TenantRepository: ITenantRepository
{
    private readonly ApplicationDbContext _context;

    public TenantRepository ( ApplicationDbContext context )
    {
        _context = context;
    }

    public Tenant? CurrentTenant
    {
        get;
        set;
    }

    public async Task FindCurrentTenantAsync ( string? hostName )
    {
        string host = hostName != null ? hostName : "";

        if ( host.Length == 0 )
        {
            CurrentTenant = null;
        }

        CurrentTenant = await _context.Tenants
            .IgnoreQueryFilters ( )
            .FirstOrDefaultAsync<Tenant>
             ( tenant => tenant.HostName.Length == host.Length
              && string.Equals ( tenant.HostName,host ) );


    }
}
