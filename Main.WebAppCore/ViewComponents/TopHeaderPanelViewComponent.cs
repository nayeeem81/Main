using Microsoft.AspNetCore.Mvc;
namespace Main.WebAppCore;

public class TopHeaderPanelViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        return View ( );
    }
}
