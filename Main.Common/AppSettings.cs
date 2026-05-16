namespace Main.Common;

public class MyConfigSettings
{
    public int EnumCountry { get; set; } 

    public int EnumCompanyName { get; set; } 

    public int EnumCategoryFor { get; set; }

    public int EnumCurrency { get; set; } 
}

public static class AppSettings
{
    public static MyConfigSettings Current { get; set; } 
        = new ( );
}
