
using Domain.Model;

using IRepository;

using Main.Common.Model;
using Main.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Repository;

public class PanelRepository: IPanelRepository
{
    private readonly BussinessAppDbContext _context;

    public PanelRepository ( BussinessAppDbContext context )
    {
        _context = context;
    }

    public async Task<bool> UpdatePanelsOrderAsync ( List<(int PageID,int PanelID,int PanelPosition)> listPanelPositions,
                                                    BaseDataModel baseDataModel )
    {

        List<int> panelIds = listPanelPositions.Select( p => p.PanelID ).ToList ();
        List<Panel> listPanels =  await _context.Panels
                                                .Where ( panel => panelIds
                                                .Contains ( panel.PanelID ))
                                                .ToListAsync<Panel> ( );

        listPanels.ForEach ( panel =>
        {

            int position = listPanelPositions.FirstOrDefault ( a => a.PanelID == panel.PanelID).PanelPosition;
            panel.PanelPosition = position;
            panel.ModifyBaseData ( baseDataModel );

        } );

        _context.Panels.UpdateRange ( listPanels );

        int result = await _context.SaveChangesAsync ( );

        return result > 0;
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

