using Domain.Model;

namespace IRepository;

public interface IProductRepository
{
    Task<bool> SaveChanges();

    Task<List<Product>> GetAllProducts();

    Task<bool> DeleteProduct(int productId);

    Task<bool> DeleteProductImage(int id, int productId);

    Task<Product> GetProductByProductID(int productId);

    Task<bool> SaveNewProduct ( Product productEntity );

    Task<bool> UpdateProduct ( Product productEntity );
}
