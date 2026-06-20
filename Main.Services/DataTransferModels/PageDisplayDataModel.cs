using Main.Common.Enums;
namespace DataTransferModel;

public class PageDisplayDataModel
{
    public PageDisplayDataModel ( )
    {
    }

    public PageDisplayDataModel ( int id,EnumPublicPage enumPublicPage,string tenantName )
    {
        PageID = id;
        EnumPublicPage = enumPublicPage;
        TenantName = tenantName;
    }

    public int PageID
    {
        get; set;
    }

    public EnumPublicPage EnumPublicPage
    {
        get; set;
    }

    public string TenantName
    {
        get; set;
    }
}
