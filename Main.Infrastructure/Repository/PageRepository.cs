using Domain.Model;

using IRepository;

using Main.Infrastructure;

using Microsoft.EntityFrameworkCore;
namespace Repository;

public class PageRepository: IPageRepository
{
    private readonly ApplicationDbContext _context;

    public PageRepository ( ApplicationDbContext context )
    {
        _context = context;
    }

    public async Task<List<Page>> GetAllPages ( )
    {
        List<Page> listPages = await _context.Pages.ToListAsync();

        return listPages.ToList ( );
    }

    public async Task<Page> GetSinglePage ( int id )
    {
        var page = await _context.Pages.FirstOrDefaultAsync<Page> (m => m.PageID == id);

        if ( page == null )
        {
            return new Page ( );
        }

        return page;
    }

    public async Task<bool> UpdatePage ( Panel panel,List<Post> listPosts )
    {
        panel.ListPosts = listPosts;

        Page? page = await _context.Pages.FirstOrDefaultAsync<Page>
                                  ( m => m.PageID == panel.PageID );

        if ( page == null )
            return false;

        page.CreatePanel ( panel );

        _context.Pages.Update ( page );

        int result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> UpdatePage ( Page page,List<Panel> listPanels )
    {
        page.ListPanels = listPanels;

        _context.Pages.Update ( page );

        int result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> PageExists ( int id )
    {
        return await _context.Pages.AnyAsync ( e => e.PageID == id );
    }
}

