
using Domain.Model;

using IRepository;

using Main.Common.Enums;
using Main.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ProductRepository: IProductRepository
{
    private readonly BussinessAppDbContext _context;

    public ProductRepository ( BussinessAppDbContext context )
    {
        _context = context;
    }

    public async Task<bool> SaveChanges ( )
    {
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<Product>> GetAllProducts ( )
    {
        return await _context.Products.ToListAsync ( );
    }

    public async Task<bool> DeleteProduct ( int productId )
    {
        var product = _context.Products.FirstOrDefault<Product>(a => a.ProductID == productId);

        if ( product != null )
        {
            _context.Products.Remove ( product );
        }

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteProductImage ( int id,int productId )
    {
        var image = await _context.ProductImageFiles.FirstOrDefaultAsync <ProductImageFile>
                                   ( a => a.ProductImageFileID == id && a.ProductID == productId );

        if ( image != null )
        {
            _context.ProductImageFiles.Remove ( image );
        }

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<Product> GetProductByProductID ( int postId )
    {
        Product? product = await _context.Products.FirstOrDefaultAsync<Product>
                                                   (a => a.ProductID == postId);

        if ( product != null )
        {
            return product;
        }

        return new Product ( );
    }

    public async Task<bool> SaveNewProduct ( Product productEntity )
    {
        _context.Products.Add ( productEntity );

        int result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> UpdateProduct ( Product productEntity )
    {
        _context.Products.Update ( productEntity );

        var result = await _context.SaveChangesAsync ();

        return result > 0;
    }

    public async Task<List<Product>> GetSelectProducts ( EnumCompanyName company )
    {
        return await _context.Products
                .Where ( a => a.HostCompanyName == company )
                .ToListAsync ( );
    }
}

