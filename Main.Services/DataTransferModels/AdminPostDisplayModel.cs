using Main.Common.Enums;

namespace DataTransferModel;

public class AdminPostDisplayModel
{
    public AdminPostDisplayModel ( )
    {
    }

    public int AdminPostID
    {
        get; set;
    }

    public string PosterName
    {
        get; set;
    }

    public string PostTitle
    {
        get; set;
    }

    public int UserID
    {
        get; set;
    }

    public EnumPostType PostType
    {
        get; set;
    }

    public string DisplayCompanyName
    {
        get; set;
    }
}
