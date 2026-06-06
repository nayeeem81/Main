namespace WebAppCore.ViewModel;

public class SearchModel
{
    public SearchModel()
    {
        Category = "Select Category";
        Key = "";
        Location = "";
        IsUrgentDeal = false;
        IsNew = false;
        IsUsed = false;
        IsTitleOnly = false;
    }

    public string Category { get; set; }

    public string Key { get; set; }

    public string Location { get; set; }

    public bool IsUrgentDeal { get; set; }

    public bool IsNew { get; set; }

    public bool IsUsed { get; set; }

    public bool IsTitleOnly { get; set; }
}
