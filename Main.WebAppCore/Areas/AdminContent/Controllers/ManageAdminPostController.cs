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

    private void SetImageInDataModel( AdminPostDataModel adminPostDataModel )
    {
        List<AdminImageFileDataModel> listAdminImageFileDataModels
                                      = new List<AdminImageFileDataModel>();

        AdminImageFileDataModel adminImageFileDataModel;

        List<ImageFile> listSessionImageFiles = GetAllSessionImages();

        listSessionImageFiles.ForEach ( imgFile =>
        {
            adminImageFileDataModel = new AdminImageFileDataModel ()
            {
                ImageFileContent = imgFile.FileContent,
                AdminPostID = imgFile.PostID ?? 0,
                AdminImageFileID = 0,
                BaseDataModel = _userContext.GetCreateBaseDataModel()
            };

            listAdminImageFileDataModels.Add ( adminImageFileDataModel );
        } );

        adminPostDataModel.ListAdminPostFileImages = listAdminImageFileDataModels;

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
        adminPostDataModel.ShortNote = adminPostViewModel.ShortNote;
        adminPostDataModel.SearchTag = adminPostViewModel.SearchTag;
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

            adminPostViewModel.PageName = "Edit Post";
            
            
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
            AdminPostDataModel adminPostDataModel = AdminPostMapping.MapAdminPostDataModel ( collection );

            SetImageInDataModel ( adminPostDataModel );

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
            AdminPostDataModel adminPostDataModel = await _adminPostService.GetAdminPostForEditPostID(id); 

            AdminPostViewModel adminPostViewModel = new AdminPostViewModel();

            AdminPostMapping.MapAdminPostViewModel ( adminPostDataModel, adminPostViewModel );

            adminPostViewModel.ListAdminPostFileImages = AdminPostMapping.MapAdminImageFileViewModelList ( adminPostDataModel.ListAdminPostFileImages );

            adminPostViewModel.PageName = "Post Details";

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

                            ImageFile objFile = new ImageFile { FileContent = imgByte, IsNew = true };
                            
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
        bool result = false;
        try
        {
            if ( postId == 0 )
            {
                result = await _adminPostService.DeleteAdminPostImage(id, postId);
            }
            else
            {
                RemoveSessionImageFile ( id );
                result = true;
            }

            return Json(new { success = result } );
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
            return BadRequest ( new
            {
                success = false
            } );
        }
    }


    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteAdminPost(int id, int fakeId)
    {
        try
        {

            var result = await _adminPostService.DeleteAdminPost(id);

            if (result)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction ( "Delete" , new { id = id }  );

        }
        catch
        {
            return BadRequest ( new
            {
                success = false
            } );
        }
    }
}

