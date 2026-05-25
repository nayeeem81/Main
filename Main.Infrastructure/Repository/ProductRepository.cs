
using Domain.Model;
using IRepository;
using Main.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ProductRepository : IProductRepository
{
    private readonly BussinessAppDbContext _Context;

    public ProductRepository( BussinessAppDbContext context )
    {
        _Context = context;
    }

    public async Task<bool> SaveChanges()
    {
        var result = await _Context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return await _Context.Products.ToListAsync();
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        var product = _Context
            .Products
            .ToList()
            .Single<Product>(a => a.ProductID == productId);

        if (product != null)
        {
            _Context.Products.Remove(product);
        }

        var result = await _Context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteProductImage(int id, int productId)
    {
        var image = await 
            _Context
            .ProductImageFiles
            .Where(a => 
                a.ProductImageFileID == id && 
                a.ProductID == productId)
            .FirstOrDefaultAsync();

        if (image != null)
        {
            _Context
                .ProductImageFiles
                .Remove(image);
        }

        var result = await _Context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<Product> GetProductByProductID(int postId)
    {
        return await _Context
            .Products
            .SingleAsync<Product>(a => a.ProductID == postId);
    }

    public async Task<bool> SaveNewProduct(Product productEntity)
    {
        _Context.Products.Add( productEntity );

        int result = await _Context.SaveChangesAsync();

        return result > 0;
    }
    
    public async Task<bool> UpdateProduct ( Product productEntity )
    {
        _Context.Products.Update ( productEntity );

        var result = await _Context.SaveChangesAsync ();

        return result > 0;
    }
}

