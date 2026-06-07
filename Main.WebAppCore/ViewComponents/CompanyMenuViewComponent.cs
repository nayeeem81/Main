using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewCompont;

public class CompanyMenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
