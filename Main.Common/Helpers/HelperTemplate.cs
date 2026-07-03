namespace Main.Common;

public static class HelperTemplate
{
    public static Dictionary<int,int> GetTemplatePostLimit ( )
    {
        Dictionary<int, int> listTemplatePostLimit = new Dictionary<int, int> ()
        {
            { (int) EnumPanelTemplate.ProductQuard, 4 },
            { (int) EnumPanelTemplate.ProductDouble, 2 },
            { (int) EnumPanelTemplate.ProductTriangle, 3 },
            { (int) EnumPanelTemplate.ProductSixer, 6 },
            { (int) EnumPanelTemplate.AdminBannerCarousel, 4 },
            { (int) EnumPanelTemplate.AdminSingleBanner, 1 }
        };

        return listTemplatePostLimit;
    }

    public static int FindTemplatePostLimit ( int enumPanelTemplate )
    {
        Dictionary<int,int>  dictLimit = GetTemplatePostLimit();
        return dictLimit[enumPanelTemplate];
    }
}
