namespace DataTransferModel;

public class ManageProductDataModel
{
    public ManageProductDataModel()
    {
        ListProduct = new List<ProductDataModel>();
    }

    public List<ProductDataModel> ListProduct { get; set; }
}
