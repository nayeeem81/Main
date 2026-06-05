namespace WebApp.ViewModel;

public class AdminPostCommentViewModel : BaseViewModel
{
    public AdminPostCommentViewModel()
    {
    }

    public int AdminPostCommentID { get; set; }
   
    public string Comment { get; set; } = string.Empty;

    public int AdminPostID { get; set; }

}
