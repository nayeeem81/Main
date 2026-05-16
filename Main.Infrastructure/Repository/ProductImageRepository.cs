using Microsoft.EntityFrameworkCore;
using Main.Model;
using Data;
using IRepository;
using Main.Common.EnumClasses;

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
            
        List<Product> list = await _context.Products
                                    .Where( a => a.HostCompanyName == company)
                                    .ToListAsync();
            
        return list;

    }
}

 