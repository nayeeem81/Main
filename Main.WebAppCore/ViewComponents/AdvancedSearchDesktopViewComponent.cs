using Main.Infrastructure;

using Microsoft.AspNetCore.Mvc;
namespace Main.WebAppCore.ViewCompont;

public class AdvancedSearchDesktopViewComponent: ViewComponent
{
    private readonly ITenantSetter _tenantSetter;

    public AdvancedSearchDesktopViewComponent ( ITenantSetter tenantSetter )
    {
        _tenantSetter = tenantSetter;
    }

    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        MenuObjectModel menuObjectModel =
            new MenuObjectModel(_tenantSetter.TenantShopType);

        return View ( menuObjectModel );
    }
}
