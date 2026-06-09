using DataTransferModel;
using Main.Common.Model;
using Main.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppCore.Helper;
using WebAppCore.ViewModel;
using WebAppCore.ViewModel.Extensions;

namespace Main.WebAppCore;

[Area("CompanyContent")]
[Authorize(Roles = "Company")]
public class ManageProductController : BaseController
{

    private readonly IProductService _productService;
    private readonly ILogger<ManageProductController> _logger; 
    private readonly IUserContext _userContext;

    public ManageProductController( IProductService productService,
        ILogger<ManageProductController> logger,
        IUserContext userContext )
    {
        _productService = productService;
        _logger = logger;
        _userContext = userContext;
    }


    [Authorize(Roles = "Company")]
    public async Task<IActionResult> Index()
    {
        try
        {
            List<ProductDisplayModel> productDataModels = await _productService.GetAllProducts();

            List<ProductDisplayViewModel> disayProductViewModels = ProductMapping.MapDisplayProductViewModel 
                ( productDataModels, _userContext.EnumCategoryFor );

            return View ( disayProductViewModels );
        }
        catch
        {
            return View (new List<ProductDisplayViewModel>() );
        }
    }


    private void SetImageInDataModel(ProductDataModel productDataModel)
    {
        List<ProductFileDataModel> listProductImageFileDataModels
                                      = new List<ProductFileDataModel>();

        BaseDataModel baseDataModel = _userContext.GetCreateBaseDataModel ( );

        ProductFileDataModel productImageFileDataModel;

        List<ImageFile> listSessionImageFiles = GetAllSessionImages();

        listSessionImageFiles.ForEach ( imgFile =>
        {
            productImageFileDataModel = new ProductFileDataModel ( baseDataModel )
            {
                ImageFileContent = imgFile.FileContent,
                ProductID = imgFile.PostID ?? 0,
                ProductImageFileID = 0
            };

            listProductImageFileDataModels.Add ( productImageFileDataModel );
        } );

        productDataModel.ImageFiles = listProductImageFileDataModels;

        ClearImageFileListSession ( );
    }


    [Authorize(Roles = "Company")]
    public IActionResult NewProduct()
    {
        try
        {
            ClearImageFileListSession ( );

            ProductViewModel objProductViewModel = new ProductViewModel();

            objProductViewModel.PageName = "New Product";
            
            return View(objProductViewModel);
        }
        catch
        {
            return View(new ProductViewModel());
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> SaveProduct(ProductViewModel collection)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            ProductDataModel productDataModel = ProductMapping.NewProductDataModel ( collection );

            productDataModel.BaseDataModel = _userContext.GetCreateBaseDataModel ( );

            SetImageInDataModel ( productDataModel );

            var result = await _productService.SaveNewProduct(productDataModel);

            string? redirectUrl = Url.Action("Index", "ManageProduct", new
            {
                Area = "CompanyContent"
            });

            return Ok ( new
            {
                success = result, urlGo = redirectUrl
            } );
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles = "Company")]
    public async Task<ActionResult> Edit(int id)
    {
        try 
        {
            ClearImageFileListSession ( );

            ProductDataModel productDataModel = await _productService.GetProductForEditProductID(id);

            ProductViewModel productViewModel = ProductMapping.MapProductViewModel ( productDataModel );

            productViewModel.PageName = "Edit Product";

            return View( productViewModel );
        }
        catch 
        {
            return View(new ProductViewModel());
        }
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> Edit(ProductViewModel collection)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try 
        {
            ProductDataModel productDataModel = ProductMapping.MapProductDataModel ( collection );

            productDataModel.BaseDataModel = _userContext.GetUpdateBaseDataModel ( );

            SetImageInDataModel ( productDataModel );

            var result = await _productService.UpdateProduct(productDataModel);

            return Ok ( new { 
                        success = true, 
                        urlGo = Url.Action ( "Index","ManageProduct", new { Area = "CompanyContent" }) 
                    } );
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }


    [Authorize(Roles = "Company")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            ProductDataModel productDataModel = await _productService.GetProductForEditProductID(id);

            ProductViewModel productViewModel = ProductMapping.MapProductViewModel ( productDataModel );

            productViewModel.SetDisplaytext ( _userContext.EnumCategoryFor );

            productViewModel.PageName = "Product Details";

            return View( productViewModel );
        }
        catch
        {
            return View(new ProductViewModel());
        }
    }


    [HttpPost]
    [Authorize(Roles = "Company")]
    public JsonResult UploadImage(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            if ( file == null || file.Length > AppSettings.Current.PostImageSize )
            {
                return Json(new { success = false });
            }
            else
            {
                ImageFile imageFile = ReadImage ( file );

                if ( imageFile.IsNew )
                {
                    SetSessionImageFile ( imageFile );
                }

                return Json ( new {  success = true } );
            }
        }

        return Json( new { success = false });
    }


    private ImageFile ReadImage ( IFormFile file )
    {
        if ( !string.IsNullOrEmpty ( file.ContentType ) && file.FileName != null )
        {
            string extension = Path.GetExtension(file.FileName).ToLower();

            if ( extension.Equals ( ".jpg" ) || extension.Equals ( ".jpeg" )

                || extension.Equals ( ".png" ) || extension.Equals ( ".gif" ) )
            {
                var imgByte = new Byte[file.Length];

                var stream = file.OpenReadStream();

                var result = stream.Read(imgByte);

                ImageFile objFile = new ImageFile ()
                {
                    FileContent = imgByte ,
                    IsNew = true ,
                    PostID = 0
                };

                return objFile;
            }
        }

        return new ImageFile ();
    }

    [HttpGet]
    [Authorize(Roles = "Company")]
    public PartialViewResult LoadImage()
    {
        try
        {
            var objImageList = GetAllSessionImages();

            var objImage = objImageList.Last();

            return PartialView("~/Areas/CompanyContent/Views/ManageProduct/_Image.cshtml", objImage);
        }
        catch
        {
            return PartialView("~/Areas/CompanyContent/Views/ManageProduct/_Image.cshtml", new ImageFile());
        }
    }


    [HttpDelete]
    [Authorize(Roles = "Company")]
    public async Task<JsonResult> ImageRemove(int id, int postId)
    {
        bool result = false;

        try
        {
            if ( postId != 0 )
            {
                result = await _productService.DeleteProductImage ( id, postId );
            }

            result = RemoveSessionImageFile ( id );

            return Json ( new { success = result } );
        }
        catch
        {
            return Json ( new { errors = false } );
        }
    }


    [HttpGet]
    [Authorize(Roles = "Company")]
    public JsonResult GetSubCategories(int id)
    {
        try
        {
            var listSubCategories = SelectListItemDropDown.GetSubCategories( _userContext.EnumCategoryFor, id );

            return Json(listSubCategories);
        }
        catch
        {
            return Json(new List<SelectListItem>());
        }
    }


    [HttpGet]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            ProductViewModel productViewModel = new ProductViewModel ();
            productViewModel.ProductID = id;

            return View ( productViewModel );
        }                                                         
        catch 
        {
            return BadRequest ( new { success = false } );
        }
    }


    [HttpGet]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> DeleteProduct(int id, int fakeId)
    {
        try
        {
            bool result = await _productService.DeleteProduct(id);

            if ( result )
            {
                return RedirectToAction ( "Index" );
            }

            return RedirectToAction ( "Delete", new { id = id } );
        }
        catch
        {
            return BadRequest ( new { success = false } );
        }
    }
}