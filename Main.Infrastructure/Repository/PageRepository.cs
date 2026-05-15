using Microsoft.EntityFrameworkCore;
using Main.Model;
using Main.Common;
using IRepository;
using Data;

namespace Repository;

public class PageRepository: IPageRepository
{
    private readonly BussinessAppDbContext _context;

    public PageRepository ( BussinessAppDbContext context )
    {
        _context = context;
    }

    public async Task<List<Page>> GetAllPages ( ) => await _context.Pages.ToListAsync<Page> ( );


    public async Task<List<Page>> GetAllPages ( EnumCompanyName company ) =>
        await _context.Pages.Where ( a => a.HostCompanyName == company ).ToListAsync<Page> ( );


    public async Task<Page?> GetSinglePage ( int? id ) 
    {
        return await _context.Pages.FirstOrDefaultAsync ( 
            m => m.PageID == (id ??  -1 ));
    }


    public async Task<bool> UpdatePage ( Page page )
    {
        if ( page == null )
        {
            return false;
        }

        _context.Update ( page );

        int result = await _context.SaveChangesAsync();

        return result > 0;
    }


    public async Task<bool> PageExists ( int id )
    {
        return await _context.Pages.AnyAsync ( e => e.PageID == id );
    }


    public async Task<bool> CreateNewContent ( Page pageContent )
    {
           
        _context.Update<Page> ( pageContent );

        int result = await _context.SaveChangesAsync ();

        return result > 0;

    }


    public async Task<PagePanel> GetContentPanel ( int paneId)
    {

        var pagePanel = await _context.PagePanels.FirstOrDefaultAsync<PagePanel> ( a => a.PanelID == paneId );

        if ( pagePanel != null ) 
            return pagePanel;

        return new PagePanel ( );
    }
}

