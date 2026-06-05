namespace WebApp.ViewModel;

public class ManageAdminPostViewModel : BaseViewModel
{
    public ManageAdminPostViewModel()
    {
        ListAdminPost = new List<AdminPostViewModel>();
    }

    public List<AdminPostViewModel> ListAdminPost { get; set; }
}
