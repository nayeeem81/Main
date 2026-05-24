namespace DataTransferModel;

public class AdminImageFileDataModel : BaseDataModel
{
    public AdminImageFileDataModel()
    {
    }

    public AdminImageFileDataModel ( byte[] imageFileContent )
    {
        ImageFileContent = imageFileContent;
    }

    public int AdminImageFileID { get; set; }
   
    public byte[] ImageFileContent { get; set; }
   
    public int AdminPostID { get; set; }
}
