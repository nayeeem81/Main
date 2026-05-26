using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Main.Services;
using ResourceLibrary;
using Main.Common.Enums;
using WebApp.ViewModel;


namespace Main.WebAppCore;

public class HomeController : BaseController
{

    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IPageService _pageService;
    private readonly IUserContext _userContext;


    public HomeController (
        IStringLocalizer<SharedResource> localizer,
        IPageService pageService,
        IUserContext userContext
        )
    {
        _localizer = localizer;
        _pageService = pageService;
        _userContext = userContext;
    }

    //[ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var pageDataModel = await _pageService.GetPageDataModel((int)EnumPublicPage.Home);

        HomeViewModel homeViewModel = new HomeViewModel("Home Page");

        return View(homeViewModel);
    }

    //public async Task<ActionResult> Notice()
    //{
    //    //if(CheckLogout())
    //    //{
    //    //    HomeViewModel objHomeModeObj = new HomeViewModel();
    //    //    objHomeModeObj.PageName = "Notice Page";
    //    //    return View(objHomeModeObj);
    //    //}

    //    //var res = await _LoggingService.LogEntirePageVisit(EnumLogType.NoticePage, StaticAppSettings.Country, HttpContext.Session.Id);
        
    //    //var resultConfigList = await _GroupPanelConfigService
    //    //                                            .GetAllPageGroupPanelConfigurations(
    //    //                                            EnumPublicPage.NoticeAndNews,
    //    //                                            Url.Action("Market", "CategoryMarket", new { subcategoryid = "SUB_CAT_ID", pageNumber = "1" }),
    //    //                                            Url.Action("ViewItemDetail", "AllItemMarket", new { postid = "POST_ITEM_ID" }),
    //    //                                            StaticAppSettings.Country,
    //    //                                            StaticAppSettings.CURRENT_TIME_SLOT,
    //    //                                            StaticAppSettings.Currency);
    //    HomeViewModel objHomeModel = new HomeViewModel()
    //    {
    //        ContentInfoViewModel = new ContentInfoViewModel()
    //        {
    //            //ListGroupPanelConfiguration = resultConfigList.Where(a =>
    //            //a.PublishStatus.HasValue &&
    //            //a.PublishStatus.Value == EnumGroupPanelStatus.Published &&
    //            //a.ShowOrHide.HasValue &&
    //            //a.ShowOrHide == EnumShowOrHide.Yes).ToList()
    //        },
    //    };
    //    objHomeModel.PageName = "Notice Page";
    //    return View(objHomeModel);
    //}

    //private bool CheckLogout()
    //{
    //    if (!User.Identity.IsAuthenticated)
    //    {
    //        ClearSessionUser();
    //        return true;
    //    }
    //    return false;
    //}

    //[ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    //public ActionResult About()
    //{
    //    HomeViewModel objHomeModel = new HomeViewModel();
    //    ViewBag.NevigationText = "Home  >  About";
    //    return View(objHomeModel);
    //}

    //[ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    //public ActionResult Contact()
    //{
    //    ContactViewModel objHomeModel = new ContactViewModel();
    //    ViewBag.NevigationText = "Home  >  Contact";
    //    objHomeModel.AV_MessageTypeList = DropDownSelectListItem.GetAllContactMessageType();
    //    objHomeModel.PageName = "Contact Us Page";
    //    return View(objHomeModel);
    //}

    //[ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    //public ActionResult FAQ()
    //{
    //    HomeViewModel objHomeModel = new HomeViewModel();
    //    ViewBag.NevigationText = "Home  >  FAQ";
    //    objHomeModel.PageName = "FAQ Page";
    //    return View(objHomeModel);
    //}

    //[ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    //public ActionResult Privacy()
    //{
    //    HomeViewModel objHomeModel = new HomeViewModel();
    //    ViewBag.NevigationText = "Home  >  Privacy";
    //    objHomeModel.PageName = "Privacy Page";
    //    return View(objHomeModel);
    //}

    //[ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    //public ActionResult Terms()
    //{
    //    HomeViewModel objHomeModel = new HomeViewModel();
    //    ViewBag.NevigationText = "Home  >  Terms";
    //    objHomeModel.PageName = "Terms Page";
    //    return View(objHomeModel);
    //}

    //[ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    //public ActionResult OurServices()
    //{
    //    HomeViewModel objHomeModel = new HomeViewModel();
    //    ViewBag.NevigationText = "Home  >  Our Services";
    //    return View(objHomeModel);
    //}

    //[ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    //public async Task<ActionResult> Resource()
    //{
    //    //var resultConfigList;
    //    //= await _GroupPanelConfigService
    //                                                //.GetAllPageGroupPanelConfigurations(
    //                                                //EnumPublicPage.Resources,
    //                                                //Url.Action("Market", "CategoryMarket", new { subcategoryid = "SUB_CAT_ID", pageNumber = "1" }),
    //                                                //Url.Action("ViewItemDetail", "AllItemMarket", new { postid = "POST_ITEM_ID" }),
    //                                                //StaticAppSettings.Country,
    //                                                //StaticAppSettings.CURRENT_TIME_SLOT,
    //                                                //StaticAppSettings.Currency);
    //    HomeViewModel objHomeModel = new HomeViewModel()
    //    {
    //        ContentInfoViewModel = new ContentInfoViewModel()
    //        {
    //            //ListGroupPanelConfiguration = resultConfigList.Where(a =>
    //            //a.PublishStatus.HasValue &&
    //            //a.PublishStatus.Value == EnumGroupPanelStatus.Published &&
    //            //a.ShowOrHide.HasValue &&
    //            //a.ShowOrHide == EnumShowOrHide.Yes).ToList()
    //        },
    //    };
    //    ViewBag.NevigationText = "Home  >  Resources";
    //    return View(objHomeModel);
    //}

    //[HttpPost]
    //public async Task<JsonResult> SaveContactMessage(ContactViewModel objContact)
    //{
    //    var isValid = ValidationService.IsValidEmail(objContact.Email);
    //    if (!isValid)
    //        return Json("EmailInvalid");
    //    try
    //    {
    //        var objEmailViewModel = _EmailService.GetContactUsViewModel(objContact);
    //        objEmailViewModel.MessageBodyHTMLText = await FindMyView(this, "_ContactUs", objEmailViewModel);
    //        _EmailService.SendContactUsEmail(objEmailViewModel);
    //        return Json("Success");
    //    }
    //    catch
    //    {
    //        return Json("SendFailed");
    //    }
    //}

    //[HttpPost]
    //public async Task<JsonResult> BrowserInfo(BrowserLogViewModel objBrowserLog)
    //{
    //    LogBrowserInfo objLog = new LogBrowserInfo()
    //    {
    //        Country = objBrowserLog.Country,
    //        CountryCode = objBrowserLog.CountryCode,
    //        City = objBrowserLog.City,
    //        Region = objBrowserLog.Region,
    //        Lon = objBrowserLog.Lon,
    //        Lat = objBrowserLog.Lat,
    //        Width = objBrowserLog.Width,
    //        Height = objBrowserLog.Height,
    //        LogDateTime = DateTime.Now,
    //        Zip = objBrowserLog.Zip
    //    };
    //    SetBrowserId(await _LoggingService.LogBrowserInfo(objLog));
    //    return Json("Success");
    //}

    //[HttpGet]
    //public async Task<JsonResult> AddComments(string comment, int postID)
    //{
    //    var resultAddComments = await _PostMangementService.AddComments(comment, postID, StaticAppSettings.Country);
    //    return Json(GetBangladeshCurrentDateTime().ToShortDateString());
    //}

    //[HttpGet]
    //public JsonResult GetSubCategories(long id)
    //{
    //    var listSubCategories = DropDownSelectListItem.GetSubCategoryList(id, StaticAppSettings.CategoryFor);
    //    return Json(listSubCategories.ToList<SelectListItem>());
    //}
}
