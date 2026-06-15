
using Domain.Model;

using IRepository;

using Main.Common.Enums;
using Main.Common.Model;
using Main.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Repository;

public class PageRepository: IPageRepository
{
    private readonly BussinessAppDbContext _context;

    public PageRepository ( BussinessAppDbContext context )
    {
        _context = context;
    }

    public async Task<List<Page>> GetAllPages ( EnumCompanyName company )
    {
        List<Page> listPages = await _context.Pages.ToListAsync();

        return listPages.Where ( a => ( int ) a.HostCompanyName == ( int ) company ).ToList ( );
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


    public async Task<bool> PageExists ( int id )
    {
        return await _context.Pages.AnyAsync ( e => e.PageID == id );
    }


    public async Task<bool> UpdatePanelsOrderAsync (
        List<(int PageID,int PanelID,int PanelPosition)> listPanelPositions,
        BaseDataModel baseDataModel )
    {
        int result = 0;
        foreach ( var panelVariable in listPanelPositions.ToList ( ) )
        {
            Panel? panel = await _context.Panels.FirstOrDefaultAsync<Panel>
                                ( a =>
                                  a.PanelID == panelVariable.PanelID
                               && a.PageID == panelVariable.PageID
                               && a.HostCompanyName == baseDataModel.HostCompanyName
                               && a.HostCountry == baseDataModel.HostCountry);

            if ( panel != null )
            {
                panel.PanelPosition = panelVariable.PanelPosition;
                panel.ModifyBaseData ( baseDataModel );
                _context.Panels.Update ( panel );
                result = await _context.SaveChangesAsync ( );
            }
        }

        return result > 0;
    }

    public async Task<bool> DeletePanelAsync (
        int panelId,int pageId,
        EnumCompanyName company,
        EnumCountry country )
    {
        Panel panel = await _context.Panels.FirstAsync<Panel>
                                ( a =>
                                  a.PanelID == panelId
                               && a.PageID == pageId
                               && a.HostCompanyName == company
                               && a.HostCountry == country);

        _context.Panels.Remove ( panel );

        int result = await _context.SaveChangesAsync();

        return result > 0;
    }
}

