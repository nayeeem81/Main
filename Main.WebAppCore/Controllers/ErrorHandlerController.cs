using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore;

public class ErrorHandlerController : Controller
{
    public ErrorHandlerController()
    { }

    public ActionResult Index()
    {
        return View();
    }
}
