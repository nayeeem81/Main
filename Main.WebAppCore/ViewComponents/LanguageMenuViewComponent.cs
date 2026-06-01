using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore;

public class LanguageMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
