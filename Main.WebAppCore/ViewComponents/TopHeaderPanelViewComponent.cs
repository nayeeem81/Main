using Main.Infrastructure;

using Microsoft.AspNetCore.Mvc;
namespace Main.WebAppCore.ViewCompont;

public class TopHeaderPanelViewComponent: ViewComponent
{
    private readonly ITenantSetter _tenantSetter;

    public TopHeaderPanelViewComponent ( ITenantSetter tenantSetter )
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
