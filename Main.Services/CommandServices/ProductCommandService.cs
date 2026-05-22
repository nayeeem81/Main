using BusinessModel;
using IRepository;
using IService;

namespace Main.Service;

public class ProductDataService : ICommandProductService
{
    private readonly IProductRepository _ProductRepository;

    public ProductDataService(IProductRepository productRepository)
    {
        _ProductRepository = productRepository;
    }

    public async Task<List<ProductDataModel>> GetAllProducts()
    {
        return await _ProductRepository.GetAllProducts();
    }

    public async Task<bool> SaveNewProduct(ProductDataModel objProductDM)
    {
        return await _ProductRepository
                        .SaveNewProduct(objProductDM, objProductDM.ImageFiles);
    }

    public async Task<ProductDataModel> GetProductForEditProductID(int productID)
    {
        return await _ProductRepository
            .GetProductByProductID(productID);
    }

    public async Task<bool> UpdateProduct(ProductDataModel  objProductVm)
    {
        return await _ProductRepository
                    .UpdateProduct ( objProductVm );
    }

    public async Task<bool> DeleteProductImage(int id, int postId)
    {
        return await _ProductRepository
                    .DeleteProductImage (id, postId);
    }

    public async Task<bool> DeleteProduct(int postId)
    {
        return await _ProductRepository.DeleteProduct(postId);
    }
}

