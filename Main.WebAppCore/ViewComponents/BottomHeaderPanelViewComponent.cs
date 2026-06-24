using Microsoft.AspNetCore.Mvc;
namespace Main.WebAppCore;

public class BottomHeaderPanelViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( MenuObjectModel model )
    {
        return View ( model );
    }
}
