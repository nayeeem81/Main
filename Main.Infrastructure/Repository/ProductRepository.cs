using BusinessModel;
using Data;
using Entity.Model;
using IRepository;

using Main.Common.Enums;

using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ProductRepository : IProductRepository
{
    private readonly BussinessAppDbContext _Context;

    public ProductRepository( BussinessAppDbContext context )
    {
        _Context = context;
    }

    public async Task<bool> SaveChanges()
    {
        var result = await _Context.SaveChangesAsync();
        return result > 0;
    }

    private void MapProductEntityToProductViewModelListModel ( Product postEntity,ProductDataModel productViewModel )
    {
        productViewModel.ProductID = postEntity.ProductID;
        productViewModel.CategoryID = postEntity.CategoryID;
        productViewModel.SubCategoryID = postEntity.SubCategoryID;
        productViewModel.ProductName = postEntity.ProductName;
        productViewModel.UnitPrice = postEntity.Price;
        productViewModel.Discount = postEntity.Discount;
    }

    public async Task<List<ProductDataModel>> GetAllProducts()
    {
        var listProducts = await _Context.Products.ToListAsync();

        List<ProductDataModel> objListPostVM = new List<ProductDataModel>();

        ProductDataModel objModel;

        foreach ( Product item in listProducts.ToList ( ) )
        {
            objModel = new ProductDataModel ( );

            MapProductEntityToProductViewModelListModel ( item,objModel );

            objListPostVM.Add ( objModel );
        }
        return objListPostVM;
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        var product = _Context.Products.ToList().Single<Product>(a => a.ProductID == productId);

        if (product != null)
        {
            _Context.Products.Remove(product);
        }

        var result = await _Context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteProductImage(int id, int productId)
    {
        var image = await _Context.ProductImageFiles.Where(a => a.ProductImageFileID == id && a.ProductID == productId).FirstOrDefaultAsync();

        if (image != null)
        {
            _Context.ProductImageFiles.Remove(image);
        }

        var result = await _Context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<ProductDataModel> GetProductByProductID(int postId)
    {
        var productEntity = await _Context.Products.SingleAsync<Product>
             (a => a.ProductID == postId);

        List<ProductFileDataModel> objListFiles = new List<ProductFileDataModel>();

        if ( productEntity.ListImageFiles != null && productEntity.ListImageFiles.Count > 0 )
        {
            productEntity.ListImageFiles.ToList ( ).ForEach ( fileEntity =>
            {
                ProductFileDataModel objFileDM = new ProductFileDataModel()
                {
                    ProductImageFileID = fileEntity.ProductImageFileID,
                    ImageFileContent = fileEntity.ImageFileContent,
                    ProductID = fileEntity.ProductID
                };
                objListFiles.Add ( objFileDM );
            } );
        }


        List<ProductCommentDataModel> objListComments = new List<ProductCommentDataModel>();

        if ( productEntity.ListComments != null && productEntity.ListComments.Count > 0 )
        {
            productEntity.ListComments.ToList ( ).ForEach ( commentEntity =>
            {
                ProductCommentDataModel objCommentDM = new ProductCommentDataModel()
                {
                    ProductCommentID = commentEntity.ProductCommentID,
                    Comment = commentEntity.Comment,
                    ProductID = commentEntity.ProductID
                };
                objListComments.Add ( objCommentDM );
            } );
        }

        ProductDataModel objModel = new ProductDataModel()
        {
            ProductID = productEntity.ProductID,
            ProductName = productEntity.ProductName,
            Discount = productEntity.Discount,
            SaleCommission = productEntity.SaleCommission,
            SearchTag = productEntity.SearchTag,
            PostType = (EnumPostType)productEntity.PostType,
            Description = productEntity.Description,
            CategoryID = productEntity.CategoryID,
            SubCategoryID = productEntity.SubCategoryID,
            UnitPrice = productEntity.Price,
            UserID = productEntity.UserID,
            ListComments = objListComments,
            ImageFiles = objListFiles
        };

        return objModel;
    }

    public async Task<bool> SaveNewProduct(ProductDataModel objPostDM, List<ProductFileDataModel> objListFiles)
    {
        Product objProductEntity = MapProductViweModelToProductEntity(objPostDM);

        objProductEntity.CreateBaseData ( objPostDM.ModelBase );

        objProductEntity.UserID = objPostDM.UserID;
        objProductEntity.User = null;

        List <ProductImageFile> objListFileEntity = MapProductViweModelToProductFileEntity(objPostDM);

        if (objPostDM != null)
        {
            objProductEntity.ListImageFiles = objListFileEntity;
            objProductEntity.ListComments = new List<ProductComment>();

            _Context.Products.Add( objProductEntity );
        }

        int result = await _Context.SaveChangesAsync();

        return result > 0;
    }

    private Product MapProductViweModelToProductEntity ( ProductDataModel productDM )
    {
        return new Product ( )
        {
            ProductName = productDM.ProductName,
            SearchTag = 
                string.IsNullOrWhiteSpace( productDM.SearchTag ) 
                    ? null 
                    : productDM.SearchTag,

            Price = productDM.UnitPrice,
            Discount = productDM.Discount,
            SaleCommission = productDM.SaleCommission,
            CategoryID = productDM.CategoryID,
            SubCategoryID = productDM.SubCategoryID,
            Description = 
                string.IsNullOrWhiteSpace ( productDM.Description) 
                ? null 
                : productDM.Description,

            PostType = EnumPostType.Product
        };
    }

    private List<ProductImageFile> MapProductViweModelToProductFileEntity 
        ( ProductDataModel productFileDM )
    {
        List<ProductImageFile> objListFileEntity = new List<ProductImageFile>();
        productFileDM.ImageFiles.ForEach ( fileVM =>
        {
            objListFileEntity.Add ( new ProductImageFile ( fileVM.ImageFileContent ) );
        } );
        return objListFileEntity;
    }
}

