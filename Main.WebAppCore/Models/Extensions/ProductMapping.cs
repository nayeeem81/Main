using DataTransferModel;                    
using Domain.Model;
using Main.Common.Enums;

namespace WebApp.ViewModel.Extensions;

public static class ProductMapping
{
    public static Product NewProductEntity (ProductDataModel productDM)
    {
        return new Product( )
        {
            ProductName = productDM.ProductName,
            SearchTag = productDM.SearchTag,
            Price = productDM.UnitPrice,
            Discount = productDM.Discount,
            SaleCommission = productDM.SaleCommission,
            CategoryID = productDM.CategoryID,
            SubCategoryID = productDM.SubCategoryID,
            Description = productDM.Description,
            PostType = EnumPostType.Product
        };
    }

    public static void MapProductDataModel(Product postEntity, ProductDataModel productDataModel)
    {
        productDataModel.ProductID = postEntity.ProductID;
        productDataModel.CategoryID = postEntity.CategoryID;
        productDataModel.SubCategoryID = postEntity.SubCategoryID;  
        productDataModel.ProductName = postEntity.ProductName;
        productDataModel.UnitPrice = postEntity.Price;
        productDataModel.Discount = postEntity.Discount;
    }

    public static List<ProductImageFile> MapProductFileEntity(ProductDataModel productFileDM)
    {
        List<ProductImageFile> objListFileEntity = new List<ProductImageFile>();

        productFileDM.ImageFiles.ForEach(fileDM =>
        {
            objListFileEntity.Add(new ProductImageFile( fileDM.ImageFileContent ));
        });

        return objListFileEntity;
    }
}
