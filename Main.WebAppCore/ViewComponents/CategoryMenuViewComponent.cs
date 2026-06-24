
using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore;

public class CategoryMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( MenuObjectModel model )
    {
        return View ( model );
    }
}
