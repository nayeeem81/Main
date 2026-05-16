using Microsoft.Extensions.Localization;

namespace Main.WebAppCore.Helper;

public static class GlobalResources
{
    public static IStringLocalizer<SharedResource> Localizer 
    { 
        get; set; 
    }
}
