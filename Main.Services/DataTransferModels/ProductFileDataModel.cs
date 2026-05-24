namespace DataTransferModel;

public class ProductFileDataModel
{
    public ProductFileDataModel() { }

    public int ProductImageFileID { get; set; }

    public byte[] ImageFileContent { get; set; }

    public int ProductID { get; set; }

    public ProductDataModel Product { get; set; }
}
