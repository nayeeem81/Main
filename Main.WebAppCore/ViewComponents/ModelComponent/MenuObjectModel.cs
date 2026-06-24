
using Main.Common.Enums;
using Main.Common.HelperServices;
using Main.Common.Model;

using Microsoft.AspNetCore.Mvc.Rendering;

using WebAppCore.Helper;

namespace Main.WebAppCore;

public class MenuObjectModel
{
    public MenuObjectModel ( )
    {
    }

    public MenuObjectModel ( bool isAdvancedSearch,EnumStoreType store )
    {
        AV_Category = DropDownListItems.GetCategoryList ( store );

        AV_SubCategory = DropDownListItems.GetSubCategoryList ( store );
    }

    public MenuObjectModel ( EnumStoreType store )
    {
        TenantStore = store;

        ListCategory = new List<TenantVariableModel> ( );
        ListSubCategory = new List<TenantVariableModel> ( );

        ListCategory = TenantStoreHelper.GetCategoryList ( store );
        ListSubCategory = TenantStoreHelper.GetSubCategoryList ( store );
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

    public List<TenantVariableModel> ListCategory
    {
        get; set;
    }

    public List<TenantVariableModel> ListSubCategory
    {
        get; set;
    }

    public EnumStoreType TenantStore
    {
        get; set;
    }
}
