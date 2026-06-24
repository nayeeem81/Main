using Microsoft.AspNetCore.Mvc;
namespace Main.WebAppCore;

public class AdvancedSearchViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        return View ( );
    }
}
