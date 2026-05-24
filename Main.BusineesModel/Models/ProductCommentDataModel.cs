
namespace DataTransferModel;

public class ProductCommentDataModel : BaseDataModel
{
    public ProductCommentDataModel() { }

    public ProductCommentDataModel(string comment) {
        Comment = comment;
    }

    public int ProductCommentID { get; set; }

    public string Comment { get; set; } = string.Empty;

    public int ProductID { get; set; }

    public ProductDataModel Product { get; set; }
}
