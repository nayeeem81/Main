using Main.Services;
using DataTransferModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebAppCore.ViewModel;
using WebAppCore.ViewModel.Extensions;
using WebAppCore.Helper;

namespace Main.WebAppCore;

[Area("AdminContent")]
[Authorize(Roles = "Admin")]
public class ManageAdminPostController : BaseController
{
    private readonly IAdminPostService _adminPostService;
    private readonly IMemoryCache _cache;
    private readonly IUserContext _userContext;
    private readonly ILogger<ManageAdminPostController> _logger;

    public ManageAdminPostController( 
        IAdminPostService adminPostService,
        IMemoryCache cache, 
        ILogger<ManageAdminPostController> logger,
        IUserContext userContext )
    {
        _adminPostService = adminPostService;
        _cache = cache;
        _logger = logger;
        _userContext = userContext;
    }

    private void SetImageInDataModel( AdminPostDataModel postDataModel )
    {
        List<AdminImageFileDataModel> listAdminImageFileDataModel
            = new List<AdminImageFileDataModel>();

        AdminImageFileDataModel  adminImageFileDataModel;

        List<ImageFile> listImageFiles = GetAllSessionImages();

        listImageFiles.ForEach ( imgFile =>
        {
            adminImageFileDataModel = new AdminImageFileDataModel
            {
                ImageFileContent = imgFile.FileContent
            };

            adminImageFileDataModel.AdminPostID = imgFile.PostID ?? 0;

            if ( imgFile.IsNew == true )
            {
                adminImageFileDataModel.BaseDataModel = _userContext.GetCreateBaseDataModel ( );

                _logger.LogWarning ( "Set New Image id: " + imgFile.FileID );
            }
            else
            {
                adminImageFileDataModel.BaseDataModel = _userContext.GetUpdateBaseDataModel ( );

                adminImageFileDataModel.AdminImageFileID = imgFile.FileID;

                _logger.LogWarning ( "Set Base Data file id: " + adminImageFileDataModel.AdminImageFileID );
            }
        } );

        postDataModel.ListAdminPostFileImages = listAdminImageFileDataModel;

        ClearImageFileListSession ( );
    }


    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Index()
    {
        try
        {
            var listAdminPosts = await _adminPostService.GetAllAdminPosts();

            List<AdminPostDisplayViewModel> listAdminPostDisplayViewModels 
                        = AdminPostMapping.MapAdminPostDisplayViewModelList
                                                        ( listAdminPosts );
            
            return View( listAdminPostDisplayViewModels );
        }
        catch
        {
            return View(new List<AdminPostDisplayViewModel>());
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public ViewResult NewAdminContent()
    {
        try
        {
            ClearImageFileListSession ( );

            var objPostViewModel = new AdminPostViewModel();
            
            objPostViewModel.PageName = "Add Admin Post";
            
            return View("~/Areas/AdminContent/Views/ManageAdminPost/NewAdmiinContent.cshtml", objPostViewModel);
        }
        catch
        {
            return View("~/Areas/AdminContent/Views/ManageAdminPost/NewAdmiinContent.cshtml", new AdminPostViewModel());
        }
    }


    [HttpPost]
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SaveNewAdminContent(AdminPostViewModel collection)
    {
        if ( !ModelState.IsValid )
        {
            return BadRequest ( error: "Invalid model state" );
        }

        try
        {
            AdminPostDataModel adminPostDataModel = MapNewDataModel ( collection );

            SetImageInDataModel ( adminPostDataModel );

            var result = await _adminPostService.SaveNewAdminPost(adminPostDataModel);

            string? redirectUrl = Url.Action("Index", "ManageAdminPost", new { Area = "AdminContent" });

            return Ok( new { success = result, urlGo = redirectUrl } );
        } 
        catch (Exception ex)
        {
            _logger.LogWarning ( "Error saving new admin post" + ex.Message );
            return BadRequest(new { success = false, message = ex.Message });
        }
    }


    private AdminPostDataModel MapNewDataModel ( AdminPostViewModel adminPostViewModel )
    {
        AdminPostDataModel adminPostDataModel = new AdminPostDataModel();

        adminPostDataModel.AdminPostID = adminPostViewModel.AdminPostID ?? 0;
        adminPostDataModel.PostTitle = adminPostViewModel.PostTitle;
        adminPostDataModel.PosterName = adminPostViewModel.PosterName;
        adminPostDataModel.PosterContactNumber = adminPostViewModel.PosterContactNumber;
        adminPostDataModel.WebsiteUrl = adminPostViewModel.WebsiteUrl;
        adminPostDataModel.BaseDataModel = _userContext.GetCreateBaseDataModel ( );

        return adminPostDataModel;
    }


    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Edit(int id)
    {
        try
        {
            ClearImageFileListSession ( );

            AdminPostDataModel adminPostDataModel = 
                await _adminPostService.GetAdminPostForEditPostID(id);

            
            var adminPostViewModel = new AdminPostViewModel();
            AdminPostMapping.MapAdminPostViewModel ( adminPostDataModel, adminPostViewModel);

            adminPostViewModel.ListAdminPostFileImages =            AdminPostMapping.MapAdminImageFileViewModelList( adminPostDataModel.ListAdminPostFileImages ); 

            adminPostViewModel.PageName = "Edit Admin Post";
            
            
            return View(adminPostViewModel);
        }
        catch
        {
            return View(new AdminPostViewModel());
        }
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AdminPostViewModel collection)
    {
        if (!ModelState.IsValid)
        {     
            return BadRequest(ModelState);
        }

        try
        {
            AdminPostDataModel adminPostDataModel = new AdminPostDataModel();

            SetImageInDataModel ( adminPostDataModel );

            adminPostDataModel = AdminPostMapping.MapAdminPostDataModel ( collection );

            adminPostDataModel.BaseDataModel = _userContext.GetUpdateBaseDataModel ( );

            bool result = await _adminPostService.UpdateAdminPost(adminPostDataModel);

            string? urlGo = Url.Action("Index", "ManageAdminPost", new { Area = "AdminContent" });

            return Ok(new { success = result, urlGo = urlGo });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }


    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Details(int id)
    {
        try
        {
            var adminPostDataModel = await _adminPostService.GetAdminPostForEditPostID(id); 

            AdminPostViewModel adminPostViewModel = new AdminPostViewModel();

            AdminPostMapping.MapAdminPostViewModel ( adminPostDataModel, adminPostViewModel );

            adminPostViewModel.ListAdminPostFileImages = AdminPostMapping.GetAdminPostViewModelImages ( adminPostDataModel.ListAdminPostFileImages );

            adminPostViewModel.PageName = "Admin Post Details";

            return View(adminPostViewModel);
        }
        catch
        {
            return View(new AdminPostViewModel());
        }
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ImageUpload(IFormFile file)
    {
        try
        {
            if (file != null && file.Length > 0)
            {

                if (file.Length > AppSettings.Current.PostImageSize )
                {
                    return Json(new { success = false });
                }
                else
                {
                    if (!string.IsNullOrEmpty(file.ContentType) && file.FileName != null)
                    {
                        string extension = Path.GetExtension(file.FileName).ToLower();

                        if (extension.Equals(".jpg") || extension.Equals(".jpeg")

                            || extension.Equals(".png") || extension.Equals(".gif"))
                        {
                            var imgByte = new Byte[file.Length];

                            var stream = file.OpenReadStream();

                            var resut = stream.ReadAsync(imgByte);

                            ImageFile objFile = new ImageFile { FileContent = imgByte };
                            
                            SetSessionImageFile(objFile);
                        }
                    }
                }
            }

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { message = ex.Message });
        }
    }


    [HttpGet]
    [Authorize(Roles = "Admin")]
    public PartialViewResult ImageLoad()
    {
        try
        {
            List<ImageFile> imageFileList = GetAllSessionImages();

            if( imageFileList == null || imageFileList.Count == 0 )
            {
                return PartialView ( "~/Areas/AdminContent/Views/ManageAdminPost/_Image.cshtml",new ImageFile ( ) );
            }

            ImageFile imageFile = imageFileList.Last();

            return PartialView("~/Areas/AdminContent/Views/ManageAdminPost/_Image.cshtml", imageFile);
        }
        catch
        {
            return PartialView("~/Areas/AdminContent/Views/ManageAdminPost/_Image.cshtml", new ImageFile());
        }
    }


    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<JsonResult> ImageRemove(int id, int postId)
    {
        try
        {
            bool result = await _adminPostService.DeleteAdminPostImage(id, postId);

            if (!result)
            {
                RemoveSessionImageFile(id);
            }

            return Json(new { success = true });
        }
        catch
        {
            return Json(new { errors = false });
        }
    }


    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var objAdminPostViewModel = await _adminPostService.GetAdminPostForEditPostID(id);

            return View(objAdminPostViewModel);
        }
        catch
        {
            return View(new AdminPostViewModel());
        }
    }


    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteAdminPost(int id, int fakeId)
    {
        AdminPostDataModel adminPostDataModel;

        try
        {
            var result = await _adminPostService.DeleteAdminPost(id);

            if (result)
            {
                return RedirectToAction("Index");
            }  
            else
            {
                adminPostDataModel = await _adminPostService.GetAdminPostForEditPostID(id);

                var adminPostViewModel = new AdminPostViewModel();
                
                AdminPostMapping.MapAdminPostViewModel ( adminPostDataModel,adminPostViewModel );

                adminPostViewModel.ListAdminPostFileImages = 
                    AdminPostMapping.MapAdminImageFileViewModelList( adminPostDataModel.ListAdminPostFileImages );

                return View( adminPostViewModel );
            }
        }
        catch
        {
            return View(new AdminPostViewModel());
        }
    }
}

