namespace WebApp.ViewModel;

public class AdminImageFileViewModel : BaseViewModel
{
    public AdminImageFileViewModel()
    {
    }

    public int AdminImageFileID { get; set; }
   
    public byte[] ImageFileContent { get; set; }
   
    public int AdminPostID { get; set; }
}
