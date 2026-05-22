using BusinessModel;

namespace IService;

public interface ICommandProductService
{
    Task<bool> SaveNewProduct(ProductDataModel objPostDm );

    Task<bool> UpdateProduct(ProductDataModel objPostDm );

    Task<bool> DeleteProductImage(int id, int productId);

    Task<bool> DeleteProduct(int productId);
}

