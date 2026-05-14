using System.ComponentModel;

namespace Main.Common
{
    public enum EnumCategoryFor
    {
        DeshiHutBazar = 1,
        FineArts = 2
    }

    public enum EnumCompanyName
    {
        [Description("Deshi Hut Bazar")]
        DeshiHutBazar = 1,

        [Description("Fine Arts")]
        FineArts = 2
    }

    public enum EnumCountry
    {
        Bangladesh = 1,
        Nigeria = 2
    }
}
