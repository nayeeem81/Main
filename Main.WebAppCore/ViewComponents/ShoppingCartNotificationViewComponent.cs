
using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore;

public class ShoppingCartNotificationViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        return View ( );
    }
}
