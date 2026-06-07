using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewCompont;

public class UserMenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
