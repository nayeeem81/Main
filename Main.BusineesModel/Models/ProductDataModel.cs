using Main.Common.EnumClasses;

namespace BusinessModel;

public class ProductDataModel : BaseDataModel
{
    public ProductDataModel()
    {
    }

    public int ProductID { get; set; }

    public int CategoryID { get; set; }

    public int SubCategoryID { get; set; }

    public EnumPostType PostType { get; set; } = EnumPostType.Product;

    public string ProductName { get; set; }

    public string? Description { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? Discount { get; set; }

    public decimal? SaleCommission { get; set; }

    public bool? IsBrandNew { get; set; }

    public int? LikeCount { get; set; }

    public string? SearchTag { get; set; }

    public int? UserID { get; set; }

    public string? CategoryText { get; set; }

    public string? SubCategoryText { get; set; }

    public List<ProductFileDataModel> ImageFiles { get; set; } = new List<ProductFileDataModel>();

    public List<ProductCommentDataModel> ListComments { get; set; } = new List<ProductCommentDataModel>();
}






























































