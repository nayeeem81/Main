using Main.Common;

namespace Main.WebAppCore.DependentServices;

public class MyConfigSettings
{
    public Country EnumCountry
    {
        get; set;
    }

    public Currency EnumCurrency
    {
        get; set;
    }

    public int PostImageSize
    {
        get; set;
    }
}
