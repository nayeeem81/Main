
using IRepository;
using Main.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Main.Infrastructure;

namespace Repository;

public class AdminPostImageRepository: IAdminPostImageRepository
{
    private readonly BussinessAppDbContext _context;

    public AdminPostImageRepository( 
        BussinessAppDbContext context ) 
    { 
        _context = context;
    }


    public async Task<List<AdminPost>> 
        GetSelectAdminPosts(EnumCompanyName company)
    {
        return await _context.AdminPosts
            .Where(a => a.HostCompanyName == company)
            .ToListAsync<AdminPost>();
    }
}

