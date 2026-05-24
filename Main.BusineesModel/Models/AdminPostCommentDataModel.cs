namespace DataTransferModel;

public class AdminPostCommentDataModel : BaseDataModel
{
    public AdminPostCommentDataModel()
    {
    }

    public int AdminPostCommentID { get; set; }
   
    public string Comment { get; set; } = string.Empty;

    public int AdminPostID { get; set; }

}
