using Microsoft.AspNetCore.Mvc;
using Main.Common.Enums;
using WebApp.Infrastructure;

namespace Main.WebAppCore;

public class AdvancedSearchDesktopViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        MenuObjectModel menuObjectModel = 
            new MenuObjectModel(
                (EnumCategoryFor)AppSettings.Current.EnumCategoryFor);

        return View(menuObjectModel);
    }
}
