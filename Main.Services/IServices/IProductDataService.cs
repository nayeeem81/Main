using Common;

namespace IService;

public interface IProductDataService
    {
        Task<List<ProductViewModel>> GetAllProducts();

        Task<bool> SaveNewProduct(ProductViewModel objPostVm);

        Task<ProductViewModel> GetProductForEditProductID(int productID);

        Task<bool> UpdateProduct(ProductViewModel objPostVm);

        Task<bool> DeleteProductImage(int id, int productId);

        Task<bool> DeleteProduct(int productId);
    }

