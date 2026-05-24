using Application.Service;
using DataTransferModel;
using Main.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApp.Infrastructure;
using WebApp.ViewModel;

namespace FineArtsWebApp;

[Area("AdminContent")]
[Authorize(Roles = "Admin")]
public class ManageAdminPostController : BaseController
{
    private readonly ICommandAdminPostService _adminPostDataService;

    private readonly IMemoryCache _cache;

    private readonly IUserContext _userContext;

    private readonly ILogger<ManageAdminPostController> _logger;

    public ManageAdminPostController( ICommandAdminPostService adminPostDataService,
        IMemoryCache cache, 
        ILogger<ManageAdminPostController> logger,
        IUserContext userContext)
    {
        _adminPostDataService = adminPostDataService;

        _cache = cache;

        _logger = logger;

        _userContext = userContext;
    }

    private void SetImageInDataModel(AdminPostDataModel postDataModel)
    {
        List<AdminImageFileDataModel> objFileList 
            = new List<AdminImageFileDataModel>();

        objFileList = GetSessionNewAdminPostImage();

        postDataModel.ListAdminPostFileImages = objFileList;

        ClearNewAdminPostImageSessions();
    }


    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Index()
    {
        try
        {
            var listAdminPosts = await _adminPostDataService.GetAllAdminPosts();

            var objManageAdminPostViewModel = new ManageAdminPostViewModel();

            objManageAdminPostViewModel.ListAdminPost = listAdminPosts;

            return View(objManageAdminPostViewModel);
        }
        catch
        {
            return View(new ManageAdminPostViewModel());
        }
    }


    [Authorize(Roles = "Admin")]
    public ViewResult NewAdminContent()
    {
        try
        {
            ClearNewAdminPostImageSessions();

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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            AdminPostDataModel postDataModel = new AdminPostDataModel();

            postDataModel = MapNewDataModel ( collection );

            SetImageInDataModel ( postDataModel );

            var result = await _adminPostDataService.SaveNewAdminPost(postDataModel);

            string? urlGo = Url.Action("Index", "ManageAdminPost", new { Area = "AdminContent" });

            return Ok(new { success = result, urlGo = urlGo });
        } 
        catch (Exception ex)
        {
            throw ex;
            //return BadRequest(new { success = false, message = ex.InnerException.Source });
        }
    }

    private AdminPostDataModel MapNewDataModel ( AdminPostViewModel collection )
    {
        AdminPostDataModel postDataModel = new AdminPostDataModel();

        postDataModel.AdminPostID 
            = collection.AdminPostID;

        postDataModel.UserID 
            = Convert.ToInt32 ( _userContext.UserId );

        postDataModel.PostTitle = collection.PostTitle;
        postDataModel.PosterName = collection.PosterName;

        postDataModel.PosterContactNumber 
            = collection.PosterContactNumber;

        postDataModel.Currency = _userContext.EnumCurrency;

        postDataModel.HostCountry = _userContext.EnumCountry;
        
        postDataModel.HostCompanyName 
            = _userContext.EnumCompanyName;

        return postDataModel;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Edit(int id)
    {
        try
        {
            ClearNewAdminPostImageSessions();

            var objAdminPostViewModel = new AdminPostViewModel();

            objAdminPostViewModel = 
               await _adminPostDataService
                     .GetAdminPostForEditPostID(id);
            
            objAdminPostViewModel.PageName 
                = "Edit Admin Post";
            
            
            return View(objAdminPostViewModel);
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
            SetImageInDataModel(collection);

            collection.SetModelBase(GetModelBaseSession(EnumModelBase.Update));

            collection.UserID = GetSessionUserId();

            bool result = await _adminPostDataService.UpdateAdminPost(collection);

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
            var objAdminPostViewModel = await _adminPostDataService.GetAdminPostForEditPostID(id);

            objAdminPostViewModel.EnumAdminPostTypeDescription = DropDownSelectListItem.GetPostTypeList().Where(pt => pt.Value == objAdminPostViewModel.PostTypeID.ToString()).Select(pt => pt.Text).FirstOrDefault();

            objAdminPostViewModel.PageName = "Admin Post Details";

            return View(objAdminPostViewModel);
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

                if (file.Length > StaticAppSettings.POST_IMAGE_SIZE)
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

                            AdminImageFileViewModel objFile = new AdminImageFileViewModel { ImageFileContent = imgByte };
                            
                            SetSessionNewAdminPostImage(objFile);
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
            var objImageList = GetSessionNewAdminPostImage();

            var objImage = objImageList.Last();

            return PartialView("~/Areas/AdminContent/Views/ManageAdminPost/_Image.cshtml", objImage);
        }
        catch
        {
            return PartialView("~/Areas/AdminContent/Views/ManageAdminPost/_Image.cshtml", new AdminImageFileViewModel());
        }
    }


    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<JsonResult> ImageRemove(int id, int postId)
    {
        try
        {
            bool result = await _adminPostDataService.DeleteAdminPostImage(id, postId);

            if (!result)
            {
                RemoveSessionNewAdminPostImage(id);
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
            var objAdminPostViewModel = await _adminPostDataService.GetAdminPostForEditPostID(id);

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
        AdminPostViewModel objAdminPostViewModel;

        try
        {
            var result = await _adminPostDataService.DeleteAdminPost(id);

            if (result)
            {
                return RedirectToAction("Index");
            }  
            else
            {
                objAdminPostViewModel = await _adminPostDataService.GetAdminPostForEditPostID(id);

                return View(objAdminPostViewModel);
            }
        }
        catch
        {
            return View(new AdminPostViewModel());
        }
    }
}

