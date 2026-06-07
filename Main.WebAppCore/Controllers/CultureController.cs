using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;

namespace Main.WebAppCore;

public class CultureController : BaseController
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    public CultureController(IStringLocalizer<SharedResource> localizer) {
        _localizer = localizer;
    }

    [HttpGet]
    public JsonResult Change(string? languageAbbrevation)
    {
        
        string cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture: languageAbbrevation ?? "en"));


        Response.Cookies.Append (
            CookieRequestCultureProvider.DefaultCookieName,
            cookieValue,
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears ( 1 ),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            }
        );

        return Json(true);
    }
}