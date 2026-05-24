
namespace DataTransferModel;

public class InformationDataModel
{
    public InformationDataModel()
    {
        ListPages = new List<PageDataModel>();
    }

    public List<PageDataModel> ListPages { get; set; }

   
    public string? AddPanelButtons { get; set; }


    public string? ViewEditButtons { get; set; }


    public string? CompanyLabel { get; set; }


    public string? PageLabel { get; set; }
}
