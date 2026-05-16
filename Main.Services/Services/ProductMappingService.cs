using BusinessModel;
using IService;
using Main.Common;
using Main.Model;

namespace Main.Service;

public class ProductMappingService : IProductMappingService
{
    public ProductMappingService()
    {
    }

    public Product MapProductViweModelToProductEntity(ProductDataModel productVM)
    {
        return new Product()
        {
            ProductName = productVM.ProductName,
            SearchTag = productVM.
            SearchTag.IsNullOrWhiteSpace() ? null : productVM.SearchTag,
            Price = productVM.UnitPrice,
            Discount = productVM.Discount,
            SaleCommission = productVM.SaleCommission,
            CategoryID = productVM.CategoryID,
            SubCategoryID = productVM.SubCategoryID,
            Description = productVM.Description.IsNullOrWhiteSpace() ? null : productVM.Description,
            PostType = EnumPostType.Product
        };
    }

    public void MapProductEntityToProductViewModelListModel(Product postEntity, ProductDataModel productViewModel)
    {
        productViewModel.ProductID = postEntity.ProductID;
        productViewModel.CategoryID = postEntity.CategoryID;
        productViewModel.SubCategoryID = postEntity.SubCategoryID;  
        productViewModel.ProductName = postEntity.ProductName;
        productViewModel.UnitPrice = postEntity.Price;
        productViewModel.Discount = postEntity.Discount;
    }

    public List<ProductImageFile> MapProductViweModelToProductFileEntity(ProductDataModel productFileVM)
    {
        List<ProductImageFile> objListFileEntity = new List<ProductImageFile>();
        productFileVM.ImageFiles.ForEach(fileVM =>
        {
            objListFileEntity.Add(new ProductImageFile(fileVM.ImageFileContent));
        });
        return objListFileEntity;
    }
}

