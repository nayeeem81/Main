using Microsoft.AspNetCore.Mvc;
using Main.Common.Enums;
using WebAppCore.Helper;

namespace Main.WebAppCore.ViewCompont;

public class AdvancedSearchDesktopViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        MenuObjectModel menuObjectModel = 
            new MenuObjectModel(
                (EnumShopType)AppSettings.Current.EnumShopType);

        return View(menuObjectModel);
    }
}
