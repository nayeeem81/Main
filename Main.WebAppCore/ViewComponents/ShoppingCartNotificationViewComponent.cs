
using Main.Common.Enums;
using Main.Common.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewCompont;

public class ShoppingCartNotificationViewComponent : ViewComponent  
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        MenuObjectModel menuObjectModel 
            = new MenuObjectModel((EnumCategoryFor)AppSettings.Current.EnumCategoryFor);

        return View(menuObjectModel);
    }
}
