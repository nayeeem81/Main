using Main.Common;
using IService;
using Main.Model;
using IRepository;
using BusinessModel;

namespace Main.Service;

public class ProductDataService : IProductDataService
{
    private readonly IProductRepository _ProductRepository;
    private readonly IProductMappingService _ProductMappingService;

    public ProductDataService(IProductRepository productRepository, IProductMappingService productMappingService)
    {
        _ProductRepository = productRepository;
        _ProductMappingService = productMappingService;
    }

    public async Task<List<ProductDataModel>> GetAllProducts()
    {
        var listProducts = await _ProductRepository.GetAllProducts();
        List<ProductDataModel> objListPostVM = new List<ProductDataModel>();
        ProductDataModel objModel;
        foreach (Product item in listProducts.ToList())
        {
            objModel = new ProductDataModel ();
                
            _ProductMappingService.MapProductEntityToProductViewModelListModel(item, objModel);

            objModel.CategoryText = objModel.GetTextCategory();
            objModel.SubCategoryText = objModel.GetTextSubCategory();

            objListPostVM.Add(objModel);
        }
        return objListPostVM;
    }

    public async Task<bool> SaveNewProduct(ProductDataModel objPostVm)
    {
        Product objProductEntity = _ProductMappingService.MapProductViweModelToProductEntity(objPostVm);

        objProductEntity.CreateBaseData(objPostVm.ModelBase);

        objProductEntity.UserID = objPostVm.UserID.Value;
        objProductEntity.User = null;

        List <ProductImageFile> objListFileEntity = _ProductMappingService.MapProductViweModelToProductFileEntity(objPostVm);

        var result = await _ProductRepository.SaveNewProduct(objProductEntity, objListFileEntity);

        return result;
    }


    public async Task<ProductDataModel> GetProductForEditProductID(int productID)
    {
        var productEntity = await _ProductRepository.GetProductByProductID(productID);

        List<ProductFileDataModel> objListFiles = new List<ProductFileDataModel>();

        if (productEntity.ListImageFiles != null && productEntity.ListImageFiles.Count > 0)
        {
            productEntity.ListImageFiles.ToList().ForEach(fileEntity =>
            {
                ProductFileDataModel objFileVM = new ProductFileDataModel()
                {
                    ProductImageFileID = fileEntity.ProductImageFileID,
                    ImageFileContent = fileEntity.ImageFileContent,
                    ProductID = fileEntity.ProductID
                };
                objListFiles.Add(objFileVM);
            });
        }


        List<ProductCommentDataModel> objListComments = new List<ProductCommentDataModel>();

        if (productEntity.ListComments != null && productEntity.ListComments.Count > 0)
        {
            productEntity.ListComments.ToList().ForEach(commentEntity =>
            {
                ProductCommentDataModel objCommentVM = new ProductCommentDataModel()
                {
                    ProductCommentID = commentEntity.ProductCommentID,
                    Comment = commentEntity.Comment,
                    ProductID = commentEntity.ProductID 
                };
                objListComments.Add(objCommentVM);
            });
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

        objModel.CategoryText = objModel.GetTextCategory();
        objModel.SubCategoryText = objModel.GetTextSubCategory();

        return objModel;
    }

    public async Task<bool> UpdateProduct(ProductDataModel objPostVm)
    {
        var product = await _ProductRepository.GetProductByProductID(objPostVm.ProductID);

        product.ModifyBaseData(objPostVm.ModelBase);

        product.UserID = objPostVm.UserID.Value;
        product.User = null;

            
        List<ProductImageFile> images = new List<ProductImageFile>();

        images.AddRange(product.ListImageFiles);    
            
        objPostVm.ImageFiles.ForEach(fileVM =>
        {
            var objFile = new ProductImageFile(fileVM.ImageFileContent);
            objFile.ProductID = product.ProductID;
            images.Add(objFile);
        });


            
        List<ProductComment> comments = new List<ProductComment>();

        comments.AddRange(product.ListComments);

        objPostVm.ListComments.ForEach(commentVM =>
        {
            var objComment = new ProductComment();
            objComment.ProductID = product.ProductID;
            objComment.Comment = commentVM.Comment;
            comments.Add(objComment);
        });

        product.ProductName = objPostVm.ProductName;
        product.Discount = objPostVm.Discount;
        product.SaleCommission = objPostVm.SaleCommission;
        product.SearchTag = objPostVm.SearchTag;
        product.PostType = EnumPostType.Product;
        product.Description = objPostVm.Description;
        product.CategoryID = objPostVm.CategoryID;
        product.SubCategoryID = objPostVm.SubCategoryID;
        product.Price = objPostVm.UnitPrice; 
        product.ListComments = comments;
        product.ListImageFiles = images;

        var result = await _ProductRepository.SaveChanges();
        return result;
    }

    public async Task<bool> DeleteProductImage(int id, int postId)
    {
        return await _ProductRepository.DeleteProductImage(id, postId);
    }

    public async Task<bool> DeleteProduct(int postId)
    {
        var resultDelete = await _ProductRepository.DeleteProduct(postId);
        return resultDelete;
    }
}

