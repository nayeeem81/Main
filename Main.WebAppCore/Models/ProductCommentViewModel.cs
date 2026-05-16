
namespace WebApp.ViewModel;

public class ProductCommentViewModel : BaseViewModel
{
    public ProductCommentViewModel() { }

    public ProductCommentViewModel(string comment) {
        Comment = comment;
    }

    public int ProductCommentID { get; set; }

    public string Comment { get; set; } = string.Empty;

    public int ProductID { get; set; }

    public ProductViewModel Product { get; set; }
}
