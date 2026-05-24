using DataTransferModel;
using Domain.Model;
using IRepository;
using Main.Services.Extensions;

namespace Main.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _ProductRepository;

    public ProductService ( IProductRepository productRepository)
    {
        _ProductRepository = productRepository;
    }

    public async Task<List<ProductDisplayModel>> GetAllProducts()
    {
        var listProducts = await _ProductRepository.GetAllProducts();

        List<ProductDisplayModel> objListPostDisplayModel
              = ProductServiceMapping.GetProductDisplayModels(listProducts);

        return objListPostDisplayModel;
    }


    public async Task<bool> 
        SaveNewProduct(ProductDataModel productDataModel)
    {
        Product productEntity = ProductServiceMapping.MapSaveProdurtEntity ( productDataModel );

            
        return await _ProductRepository
                    .SaveNewProduct( productEntity );
    }

    public async Task<ProductDataModel> GetProductForEditProductID(int productID)
    {
        var productEntity 
            = await _ProductRepository
            .GetProductByProductID(productID);

        ProductDataModel productDataModel =
            ProductServiceMapping.MapSingelProductDataModel(productEntity);

        return productDataModel;
    }


    public async Task<bool> UpdateProduct(ProductDataModel productDataModel )
    {

        Product productEntity
            = await _ProductRepository
            .GetProductByProductID(productDataModel.ProductID);

        productEntity
            = ProductServiceMapping.MapProductUpdateEntity
            ( productEntity, productDataModel );

        return await _ProductRepository.UpdateProduct( productEntity );
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

