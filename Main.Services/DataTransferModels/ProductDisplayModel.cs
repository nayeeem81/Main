using Main.Common.Enums;
namespace DataTransferModel;

public class ProductDisplayModel
{
    public ProductDisplayModel ( )
    {
    }

    public int ProductID { get; set; }

    public int CategoryID { get; set; }

    public int SubCategoryID { get; set; }

    public string? CategoryText
    {
        get; set;
    }

    public string? SubCategoryText
    {
        get; set;
    }

    public EnumPostType PostType { get; set; }

    public string DisplayPostType { get; set; }

    public string ProductName { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? Discount { get; set; }

}






























































