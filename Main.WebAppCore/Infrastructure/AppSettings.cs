using Main.Common.Enums;

namespace WebApp.Infrastructure;

public class MyConfigSettings
{
    public EnumCountry EnumCountry { get; set; } 

    public EnumCompanyName EnumCompanyName { get; set; } 

    public EnumCategoryFor EnumCategoryFor { get; set; }

    public EnumCurrency EnumCurrency { get; set; } 

    public int SeedUserId { get; set; }
}

public static class AppSettings
{
    public static MyConfigSettings Current { get; set; } 
        = new ( );
}
