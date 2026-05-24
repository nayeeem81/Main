namespace DataTransferModel;

public class ManageProductDataModel : BaseDataModel
{
    public ManageProductDataModel()
    {
        ListProduct = new List<ProductDataModel>();
    }

    public List<ProductDataModel> ListProduct { get; set; }
}
