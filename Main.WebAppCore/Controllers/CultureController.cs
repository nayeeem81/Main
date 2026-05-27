using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;
using System.Globalization;

namespace Main.WebAppCore
{
    public class CultureController : BaseController
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        public CultureController(IStringLocalizer<SharedResource> localizer) {
            _localizer = localizer;
        }

        [HttpGet]
        public JsonResult Change(string LanguageAbbrevation)
        {
            if (LanguageAbbrevation != null)
            {
                var cultureInfo = new CultureInfo(LanguageAbbrevation);

                // Updates the current thread's culture
                CultureInfo.CurrentCulture = cultureInfo;
                CultureInfo.CurrentUICulture = cultureInfo;
            }

            Response.Cookies.Append(
                               CookieRequestCultureProvider.DefaultCookieName,
                               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture( culture: LanguageAbbrevation )),
                               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            Response.Cookies.Append("Language", LanguageAbbrevation);

            return Json(true);
        }
    }
}