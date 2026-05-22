using BusinessModel;
using IRepository;
using Main.Services.IServices;

namespace Main.Service;

public class ProductQueryService : IQueryProductService
{
    private readonly IProductRepository _ProductRepository;

    public ProductQueryService ( IProductRepository productRepository)
    {
        _ProductRepository = productRepository;
    }

    public async Task<List<ProductDataModel>> GetAllProducts()
    {
        return await _ProductRepository.GetAllProducts();
    }

    public async Task<ProductDataModel> GetProductForEditProductID(int productID)
    {
        return await _ProductRepository
            .GetProductByProductID(productID);
    }
}

