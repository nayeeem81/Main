using Main.Common.Enums;
using Main.Common.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

using WebApp.ViewModel;

namespace Main.WebAppCore;

public class MenuObjectModel
{
    public MenuObjectModel(EnumCategoryFor categoryFor)
    {
        ListCatSubCategory = 
            new List<ParentChildVriableModel> ( );

        AV_Category = SelectListItemDropDown.GetCategoryList (categoryFor);

        AV_SubCategory = SelectListItemDropDown.GetSubCategoryList ( categoryFor );
        
        AV_State = SelectListItemDropDown.GetAllStateList();
    }

    public bool IsAdminUser { get; set; }

    public bool IsVerifiedUser { get; set; }


    public bool IsAdvertiserUser { get; set; }

    public bool IsSignedInUser { get; set; }

    public bool IsLoggedOutUser { get; set; }

    public bool IsPublicUser { get; set; }

    public string ClientName { get; set; }

    public long? StateID { get; set; }

    public long? CategoryID { get; set; }

    public long? SubCategoryID { get; set; }

    public long UserID { get; set; }

    public long? PriceLow { get; set; }

    public long? PriceHigh { get; set; }

    public string SearchKey { get; set; }

    public string SimpleSearchKey { get; set; }

    public bool IsBrandNew { get; set; }

    public bool IsUsed { get; set; }

    public bool IsUrgent { get; set; }

    public bool IsPrivateSeller { get; set; }

    public bool IsCompanySeller { get; set; }

    public string AreaDescription { get; set; }

    public bool IsForSell { get; set; }

    public bool IsForRent { get; set; }

    public string SearchTag { get; set; }

    public string InitCategoryText { get; set; }

    public IEnumerable<SelectListItem> AV_State 
    { 
        get; set; 
    }

    public IEnumerable<SelectListItem> AV_Category 
    { 
        get; set; 
    }

    public IEnumerable<SelectListItem> AV_SubCategory 
    { 
        get; set; 
    }

    public List<ParentChildVriableModel> 
        ListCatSubCategory { get; set; } 
            = new List<ParentChildVriableModel> ();
}
