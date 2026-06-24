using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore;

public class AdminMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        return View ( );
    }
}
