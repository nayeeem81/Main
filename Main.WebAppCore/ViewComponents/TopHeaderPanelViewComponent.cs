using Main.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using WebAppCore.Helper;

namespace Main.WebAppCore.ViewCompont;

public class TopHeaderPanelViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        MenuObjectModel menuObjectModel 
            = new MenuObjectModel((EnumShopType)AppSettings.Current.EnumShopType);

        return View(menuObjectModel);
    }
}
