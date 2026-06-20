using Domain.Model;

using IRepository;

using Main.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Repository;

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

    public async Task FindCurrentTenant ( string? hostName )
    {
        string host = hostName != null ? hostName : "";

        Tenant? tenant = null;

        if ( host.Length == 0 )
        {
            CurrentTenant = null;
        }


        tenant = await _context.Tenants.FirstOrDefaultAsync<Tenant> (
            tenant => tenant.Domain.Length == host.Length &&
            tenant.Equals ( host ) );

        if ( tenant == null )
        {
            CurrentTenant = null;
        }
        else
        {
            CurrentTenant = tenant;
        }
    }
}
