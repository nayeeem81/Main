using BusinessModel;

namespace Main.Services.IServices;

public interface IQueryProductService
{
    Task<List<ProductDataModel>> GetAllProducts();

    Task<ProductDataModel> GetProductForEditProductID(int productID);
}

