using Main.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace WebApp.Infrastructure;

public partial class BaseController : Controller
{
    public readonly KeyValuePair<int, EnumPublicPage> CurrentPage;

    private readonly ICompositeViewEngine _viewEngine;
    
    public BaseController()
    { 
    }

    public DateTime GetBangladeshCurrentDateTime()
    {
        var BnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
        DateTime BaTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, BnTimeZone);
        return BaTime;
    }

    public ActionResult UnwantedAccessError()
    {
        return View("UnwantedAccessError");
    }

    public ActionResult CheckLogoutRequirements()
    {
        return RedirectToAction("Index", "Home");
    }
}