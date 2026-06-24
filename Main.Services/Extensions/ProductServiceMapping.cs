using DataTransferModel;
using Domain.Model;
using Main.Common.Enums;

namespace Main.Services.Extensions;

public static class ProductServiceMapping
{
    public static List<ProductDisplayModel> GetProductDisplayModels (List<Product> listProducts)
    {
        List<ProductDisplayModel> objListPostDisplayModel
            = new();

        ProductDisplayModel objProductDisplayModel;

        foreach ( Product item in listProducts.ToList () )
        {
            objProductDisplayModel = new ProductDisplayModel ();

            MapProductDisplayModel (item,objProductDisplayModel);

            objListPostDisplayModel.Add (objProductDisplayModel);
        }

        return objListPostDisplayModel;
    }

    private static void MapProductDisplayModel (
        Product productEntity,ProductDisplayModel productDisplayModel)
    {
        productDisplayModel.ProductID = productEntity.ProductID;
        productDisplayModel.CategoryID = productEntity.CategoryID;
        productDisplayModel.SubCategoryID = productEntity.SubCategoryID;
        productDisplayModel.ProductName = productEntity.ProductName;
        productDisplayModel.UnitPrice = productEntity.Price;
        productDisplayModel.Discount = productEntity.Discount;
    }

    public static Product MapSaveProductEntity (ProductDataModel productDataModel)
    {
        Product productEntity = CreateProductEntity(productDataModel);

        productEntity.CreateBaseData (productDataModel.BaseDataModel);

        List <ProductImageFile> objListFileEntity
            = MapProductFileEntity(productDataModel);

        if ( productDataModel != null )
        {
            productEntity.ListImageFiles = objListFileEntity;
            productEntity.ListComments = new List<ProductComment> ();
        }

        return productEntity;
    }

    private static Product CreateProductEntity (ProductDataModel productDataModel)
    {
        return new Product ()
        {
            ProductName = productDataModel.ProductName,
            SearchTag =
                string
                .IsNullOrWhiteSpace (productDataModel.SearchTag)
                ? null
                : productDataModel.SearchTag,

            Price = productDataModel.UnitPrice,
            Discount = productDataModel.Discount,
            SaleCommission = productDataModel.SaleCommission,
            CategoryID = productDataModel.CategoryID,
            SubCategoryID = productDataModel.SubCategoryID,
            Description = string
                    .IsNullOrWhiteSpace (productDataModel.Description)
                    ? null
                    : productDataModel.Description,

            PostType = EnumPostType.Product
        };
    }

    private static List<ProductImageFile> MapProductFileEntity
        (ProductDataModel productDataModel)
    {
        List<ProductImageFile> listProductFileEntity
            = new();

        ProductImageFile productImageFile;
        productDataModel.ImageFiles.ForEach (fileDataModel =>
        {
            productImageFile = new ProductImageFile (fileDataModel.ImageFileContent);
            productImageFile.ProductID = fileDataModel.ProductID;
            productImageFile.CreateBaseData (fileDataModel.BaseDataModel);
            listProductFileEntity.Add (productImageFile);
        });

        return listProductFileEntity;
    }

    public static ProductDataModel MapSingleProductDataModel (Product productEntity)
    {
        if ( productEntity == null )
        {
            return new ProductDataModel ();
        }

        List<ProductFileDataModel> listProductFilesDataModel =
            new();

        if ( productEntity.ListImageFiles != null
            && productEntity.ListImageFiles.Count > 0 )
        {

            productEntity.ListImageFiles.ToList ().ForEach (fileEntity =>
                {
                    ProductFileDataModel fileDataModel = new()
                    {
                        ProductImageFileID = fileEntity.ProductImageFileID,
                        ImageFileContent = fileEntity.ImageFileContent,
                        ProductID = fileEntity.ProductID
                    };

                    listProductFilesDataModel.Add (fileDataModel);

                });
        }

        List<ProductCommentDataModel> listCommentsDataModel
            = new();

        if ( productEntity.ListComments != null
             && productEntity.ListComments.Count > 0 )
        {

            productEntity.ListComments.ToList ().ForEach (commentEntity =>
            {
                ProductCommentDataModel objCommentDataModel = new()
                {
                    ProductCommentID = commentEntity.ProductCommentID,
                    Comment = commentEntity.Comment,
                    ProductID = commentEntity.ProductID
                };

                listCommentsDataModel.Add (objCommentDataModel);

            });
        }

        ProductDataModel productDataModel = new()
        {
            ProductID = productEntity.ProductID,
            ProductName = productEntity.ProductName,
            Discount = productEntity.Discount.HasValue ? productEntity.Discount.Value : 0,
            SaleCommission = productEntity.SaleCommission.HasValue ? productEntity.SaleCommission.Value : 0,
            SearchTag = productEntity.SearchTag,
            PostType =   productEntity.PostType ,
            Description = productEntity.Description,
            CategoryID = productEntity.CategoryID,
            SubCategoryID = productEntity.SubCategoryID,
            UnitPrice = productEntity.Price,
            ListComments = listCommentsDataModel,
            ImageFiles = listProductFilesDataModel
        };

        return productDataModel;
    }

    public static Product MapProductUpdateEntity (Product productEntity,ProductDataModel productDataModel)
    {
        List<ProductImageFile> listProductImageFileEntity
            = new();

        listProductImageFileEntity.AddRange (productEntity.ListImageFiles);

        ProductImageFile fileEntity;

        productDataModel.ImageFiles.ForEach (fileDataModel =>
        {
            fileEntity = new ProductImageFile (fileDataModel.ImageFileContent);

            fileEntity.CreateBaseData (fileDataModel.BaseDataModel);

            fileEntity.ProductID = productEntity.ProductID;

            listProductImageFileEntity.Add (fileEntity);

        });

        List<ProductComment> listProductCommentsEntity
            = new();

        listProductCommentsEntity.AddRange (productEntity.ListComments);


        productDataModel.ListComments.ForEach (commentDataModel =>
        {
            var commentEntity = new ProductComment();

            commentEntity.ProductID = productEntity.ProductID;
            commentEntity.Comment = commentEntity.Comment;

            listProductCommentsEntity.Add (commentEntity);
        });

        productEntity.ProductName = productDataModel.ProductName;
        productEntity.Discount = productDataModel.Discount;
        productEntity.SaleCommission = productDataModel.SaleCommission;
        productEntity.SearchTag = productDataModel.SearchTag;
        productEntity.PostType = EnumPostType.Product;
        productEntity.Description = productDataModel.Description;
        productEntity.CategoryID = productDataModel.CategoryID;
        productEntity.SubCategoryID = productDataModel.SubCategoryID;
        productEntity.Price = productDataModel.UnitPrice;

        productEntity.ListComments = listProductCommentsEntity;
        productEntity.ListImageFiles = listProductImageFileEntity;

        productEntity.ModifyBaseData (productDataModel.BaseDataModel);

        return productEntity;
    }
}
