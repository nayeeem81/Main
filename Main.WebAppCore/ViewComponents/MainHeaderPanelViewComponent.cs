using Microsoft.AspNetCore.Mvc;
namespace Main.WebAppCore;

public class MainHeaderPanelViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        return View ( );
    }
}

