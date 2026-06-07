using Main.Common.Model;

namespace DataTransferModel;

public class AdminImageFileDataModel : DataModel
{
    public AdminImageFileDataModel()
    {
        BaseDataModel = new BaseDataModel();
    }

    public AdminImageFileDataModel ( byte[] imageFileContent )
    {
        ImageFileContent = imageFileContent;
        BaseDataModel = new BaseDataModel ( );
    }

    public int AdminImageFileID { get; set; }
   
    public byte[] ImageFileContent { get; set; }
   
    public int AdminPostID { get; set; }
}
