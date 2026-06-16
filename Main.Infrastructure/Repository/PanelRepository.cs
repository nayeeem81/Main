
using Domain.Model;

using IRepository;

using Main.Infrastructure;

namespace Repository;

public class PanelRepository: IPanelRepository
{
    private readonly BussinessAppDbContext _context;

    public PanelRepository ( BussinessAppDbContext context )
    {
        _context = context;
    }

    public async Task<bool> DeletePanelAsync ( int panelId )
    {
        Panel? panel = await _context.Panels.FindAsync ( panelId );

        if ( panel != null )
        {
            _context.Panels.Remove ( panel );
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        return false;
    }
}

