using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewCompont;

public class LanguageMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
