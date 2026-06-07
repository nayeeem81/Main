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
                (EnumCategoryFor)AppSettings.Current.EnumCategoryFor);

        return View(menuObjectModel);
    }
}
