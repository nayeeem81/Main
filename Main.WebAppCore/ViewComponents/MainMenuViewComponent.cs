using Main.Infrastructure;

using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore.ViewCompont;

public class MainMenuViewComponent: ViewComponent
{
    private readonly ITenantSetter _tenantSetter;

    public MainMenuViewComponent ( ITenantSetter tenantSetter )
    {
        _tenantSetter = tenantSetter;
    }

    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        MenuObjectModel menuObjectModel
            = new MenuObjectModel(_tenantSetter.TenantShopType);

        return View ( menuObjectModel );
    }
}
