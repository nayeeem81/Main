using Microsoft.Extensions.Localization;

namespace ResourceLibrary.Resources;
                              
public static class GlobalResources
{
    public static IStringLocalizer<SharedResource> Localizer 
    { 
        get; set; 
    }
}
