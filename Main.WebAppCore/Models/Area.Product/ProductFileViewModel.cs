
namespace WebApp.ViewModel;

public class ProductFileViewModel
{
    public ProductFileViewModel() { }

    public int ProductImageFileID { get; set; }

    public byte[] ImageFileContent { get; set; }

    public int ProductID { get; set; }

    public ProductViewModel Product { get; set; }
}
