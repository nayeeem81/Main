using Microsoft.EntityFrameworkCore;
using IRepository;
using Main.Model;
using Main.Common;
using Data;

namespace Repository;

public class AdminPostImageRepository: IAdminPostImageRepository
{

    private readonly BussinessAppDbContext _context;

    public AdminPostImageRepository( BussinessAppDbContext context ) 
    { 
        _context = context;
    }


    public async Task<List<AdminPost>> GetSelectAdminPosts(EnumCompanyName company)
    {
        List<AdminPost> list = await _context.AdminPosts
                                        .Where(a => a.HostCompanyName == company)
                                        .ToListAsync<AdminPost>();

        return list;
    }
}

