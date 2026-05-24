namespace DataTransferModel;

public class ManageAdminPostDataModel
{
    public ManageAdminPostDataModel ( ) => ListAdminPost = new List<AdminPostDataModel> ( );

    public List<AdminPostDataModel> ListAdminPost { get; set; }
}
