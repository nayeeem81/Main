using Domain.Model;
using IRepository;
using Main.Common.Enums;
using Main.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ProductImageRepository : IProductImageRepository
{
    private readonly BussinessAppDbContext _context;

    public ProductImageRepository( BussinessAppDbContext context )
    {
        _context = context;
    }

    public async Task<List<Product>> GetSelectProducts(EnumCompanyName company) 
    {  
        return await _context.Products
                .Where( a => a.HostCompanyName == company)
                .ToListAsync();
    }
}

 