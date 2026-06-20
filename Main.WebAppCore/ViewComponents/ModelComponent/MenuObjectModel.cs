using Main.Common.Enums;
using Main.Common.Model;

using Microsoft.AspNetCore.Mvc.Rendering;

using WebAppCore.Helper;

namespace Main.WebAppCore.ViewCompont;

public class MenuObjectModel
{
    public MenuObjectModel ( )
    {
    }

    public MenuObjectModel ( EnumShopType tenantShopType )
    {
        ListCatSubCategory = new List<ParentChildVriableModel> ( );

        AV_Category = DropDownListItems.GetCategoryList ( tenantShopType );

        AV_SubCategory = DropDownListItems.GetSubCategoryList ( tenantShopType );
    }

    public string ClientName
    {
        get; set;
    }

    public long? CategoryID
    {
        get; set;
    }

    public long? SubCategoryID
    {
        get; set;
    }

    public string SearchKey
    {
        get; set;
    }

    public string SimpleSearchKey
    {
        get; set;
    }

    public string SearchTag
    {
        get; set;
    }

    public string CategoryText
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

    public List<ParentChildVriableModel> ListCatSubCategory
    {
        get; set;
    }
}
