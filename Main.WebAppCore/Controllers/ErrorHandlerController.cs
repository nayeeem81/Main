using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.Controllers;

public class ErrorHandlerController: Controller
{
    public ErrorHandlerController ()
    {
    }

    public ActionResult Index ()
    {
        return View ();
    }
}
