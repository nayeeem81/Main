using Microsoft.AspNetCore.Mvc;
namespace Main.WebAppCore;

public class MainMenuViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        return View ( );
    }
}
