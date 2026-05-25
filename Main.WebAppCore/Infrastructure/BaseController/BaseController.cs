using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore;

public partial class BaseController : Controller
{
    public BaseController()
    { 
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