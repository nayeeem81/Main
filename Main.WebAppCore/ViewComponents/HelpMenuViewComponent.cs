using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewCompont;

public class HelpMenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
