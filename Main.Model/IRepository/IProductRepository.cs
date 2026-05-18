using BusinessModel;

namespace IRepository;

public interface IProductRepository
{
    Task<bool> SaveChanges();

    Task<List<ProductDataModel>> GetAllProducts();

    Task<bool> DeleteProduct(int productId);

    Task<bool> DeleteProductImage(int id, int productId);

    Task<ProductDataModel> GetProductByProductID(int productId);

    Task<bool> SaveNewProduct(ProductDataModel productObject, List<ProductFileDataModel> objListFiles);
}
