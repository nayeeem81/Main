using Main.Common.Enums;
using Main.Common.Model;

namespace DataTransferModel;

public class PostDataModel: DataModel
{
    public PostDataModel ( )
    {
        BaseDataModel = new BaseDataModel ( );
    }

    public int PanelPostID
    {
        get; set;
    }

    public EnumPostType EnumPostType
    {
        get; set;
    }

    public int RootID
    {
        get; set;
    }

    public int PostOrder
    {
        get; set;
    }

    public int ImageOrderID
    {
        get; set;
    }

    public int ImageFileID
    {
        get; set;
    }

    public int CategoryID
    {
        get; set;
    }

    public byte[] ImageFileContent
    {
        get; set;
    }

    public string PostTitle
    {
        get; set;
    }

    public string PostDescription
    {
        get; set;
    }

    public decimal Price
    {
        get; set;
    }

    public string? WebsiteUrl
    {
        get; set;
    }

    public int PageID
    {
        get; set;
    }
}
