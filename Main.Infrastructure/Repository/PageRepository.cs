
using IRepository;
using Main.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Main.Infrastructure;

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
        return await _context.Pages
            .Where ( a => a.HostCompanyName == company )
            .ToListAsync<Page> ( );
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

    public async Task<bool> UpdatePage ( Page? page )
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
}

