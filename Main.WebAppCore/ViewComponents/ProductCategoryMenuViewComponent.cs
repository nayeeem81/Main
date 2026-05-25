using Main.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using WebApp.Infrastructure;

namespace Main.WebAppCore
{
    public class ProductCategoryMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            MenuObjectModel menuObjectModel
                = new MenuObjectModel((EnumCategoryFor)AppSettings.Current.EnumCategoryFor);

            return View(menuObjectModel);
        }
    }
}
