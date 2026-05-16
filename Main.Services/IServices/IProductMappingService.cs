using BusinessModel;
using Main.Model;

namespace IService;

public interface IProductMappingService
    {
        Product MapProductViweModelToProductEntity(ProductDataModel productVM);

        void MapProductEntityToProductViewModelListModel(Product postEntity, ProductDataModel productViewModel);

        List<ProductImageFile> MapProductViweModelToProductFileEntity(ProductDataModel productFileVM);
    }

