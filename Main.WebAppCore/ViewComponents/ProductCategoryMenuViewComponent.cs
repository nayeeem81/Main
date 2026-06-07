using Main.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using WebAppCore.Helper;

namespace Main.WebAppCore.ViewCompont;

public class ProductCategoryMenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        MenuObjectModel menuObjectModel
            = new MenuObjectModel((EnumCategoryFor)AppSettings.Current.EnumCategoryFor);

        return View(menuObjectModel);
    }
}
