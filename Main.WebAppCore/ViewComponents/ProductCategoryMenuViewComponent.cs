
using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore;

public class ProductCategoryMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        return View ( );
    }
}
