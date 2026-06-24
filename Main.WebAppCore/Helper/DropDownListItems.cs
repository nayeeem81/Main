using Main.Common.Enums;
using Main.Common.HelperServices;
using Main.Common.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppCore.Helper;

public class DropDownListItems
{
    public DropDownListItems ( )
    {
    }


    //   [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetPostTypeList ( )
    {
        var listCountries = ListEnum.GetPostTypeList().OrderBy(a => a.Text).ToList();
        List<SelectListItem> objOfferTypeListItems = new List<SelectListItem>();
        foreach ( var item in listCountries )
        {
            SelectListItem objItem = new SelectListItem
            {
                Text = item.Text,
                Value = item.ValueID.ToString ( )
            };
            objOfferTypeListItems.Add ( objItem );
        }
        return objOfferTypeListItems.AsEnumerable ( );
    }


    [ResponseCache ( CacheProfileName = "Cache1dayServerNBrowser" )]
    public static IEnumerable<SelectListItem> GetAdminPostTypeList ( )
    {
        var listCountries = ListEnum.GetAdminPostTypeList().ToList();

        List<SelectListItem> objOfferTypeListItems = new List<SelectListItem>();
        SelectListItem objItem;

        foreach ( var item in listCountries )
        {
            objItem = new SelectListItem
            {
                Text = item.Text,
                Value = item.ValueID.ToString ( )
            };
            objOfferTypeListItems.Add ( objItem );
        }

        return objOfferTypeListItems.AsEnumerable ( );
    }


    [ResponseCache ( CacheProfileName = "Cache1dayServerNBrowser" )]
    public static IEnumerable<SelectListItem> GetPageList ( )
    {
        var listCountries = ListEnum.GetPublicPages().OrderBy(a => a.Text).ToList();
        List<SelectListItem> objCoutryListItems = new List<SelectListItem>();
        foreach ( var item in listCountries )
        {
            SelectListItem objItem = new SelectListItem
            {
                Text = item.Text,
                Value = item.ValueID.ToString ( )
            };
            objCoutryListItems.Add ( objItem );
        }
        return objCoutryListItems.AsEnumerable ( );
    }


    [ResponseCache ( CacheProfileName = "Cache1dayServerNBrowser" )]
    public static IEnumerable<SelectListItem> GetPanelTempletList ( )
    {
        var listColumns = ListEnum.GetPanelTempletList().OrderBy(a => a.Text).ToList();

        List<SelectListItem> objCurrencyListItems = new List<SelectListItem>();

        foreach ( var item in listColumns )
        {
            SelectListItem objItem = new SelectListItem
            {
                Text = item.Text,
                Value = item.ValueID.ToString()
            };
            objCurrencyListItems.Add ( objItem );
        }

        SelectListItem objItem1 = new SelectListItem
        {
            Text = "",
            Value = ""
        };

        objCurrencyListItems.Add ( objItem1 );

        return objCurrencyListItems.AsEnumerable ( );
    }

    [ResponseCache ( CacheProfileName = "Cache1dayServerNBrowser" )]
    public static IEnumerable<SelectListItem> GetCurrencyList ( )
    {
        var listCurrency = ListEnum.GetCurrencyList().OrderBy(a => a.Text).ToList();

        List<SelectListItem> objCurrencyListItems = [new SelectListItem ( ) { Value = null,Text = "" }];

        foreach ( TenantVariableModel? item in listCurrency )
        {
            SelectListItem objItem = new SelectListItem
            {
                Text = item.Text,
                Value = item.ValueID.ToString ( )
            };
            objCurrencyListItems.Add ( objItem );
        }

        return objCurrencyListItems.AsEnumerable ( );
    }

    [ResponseCache ( CacheProfileName = "Cache1dayServerNBrowser" )]
    public static IEnumerable<SelectListItem> GetCountryList ( )
    {
        var listCountries = ListEnum.GetCountryList().OrderBy(a => a.Text).ToList();

        List<SelectListItem> objCountryListItems = new List<SelectListItem>();

        foreach ( TenantVariableModel? item in listCountries )
        {
            SelectListItem objItem = new SelectListItem
            {
                Text = item.Text,
                Value = item.ValueID.ToString ( )
            };

            objCountryListItems.Add ( objItem );
        }

        return objCountryListItems.AsEnumerable ( );
    }

    // [ResponseCache(CacheProfileName = "Cache30Mins")]
    public static IEnumerable<SelectListItem> GetSubCategoryList ( EnumStoreType store )
    {
        var listSubCat = new List<TenantVariableModel>();

        return GetSelectList ( TenantStoreHelper.GetSubCategoryList ( store ),"" );
    }

    //[ResponseCache ( CacheProfileName = "Cache30Mins" )]
    public static IEnumerable<SelectListItem> GetSubCategories
    ( EnumStoreType store,int categoryId )
    {
        return GetSelectList
        ( TenantStoreHelper.GetSubCategoryListByID ( categoryId,store ),"" );
    }

    // [ResponseCache ( CacheProfileName = "Cache1dayServerNBrowser" )]
    public static IEnumerable<SelectListItem> GetShowHideList ( )
    {
        var listShowHideList = ListEnum.GetShowHideList();

        List<SelectListItem> objListItems = new List<SelectListItem>();

        listShowHideList.ForEach ( a =>
        {
            SelectListItem objItem = new SelectListItem
            {
                Text = a.Text.Trim(),
                Value = a.ValueID.ToString().Trim()
            };

            objListItems.Add ( objItem );
        } );

        return objListItems.AsEnumerable ( );
    }

    private static IEnumerable<SelectListItem> GetSelectList ( List<TenantVariableModel> listTenantVariableModel,string selectText )
    {
        List<SelectListItem> objList =
            new List<SelectListItem>()
            {
                new SelectListItem() {
                    Text = selectText,
                    Value = null,
                    Selected = true
                } };

        listTenantVariableModel.ForEach ( a =>
        {
            SelectListItem objItem = new SelectListItem
            {
                Text = a.Text.Trim(),
                Value = a.ValueID.ToString().Trim()
            };

            objList.Add ( objItem );
        } );

        return objList.AsEnumerable ( );
    }

    public static IEnumerable<SelectListItem> GetSelectList ( List<TenantVariableModel> listTenantVariableModel )
    {
        List<SelectListItem> objList =
                            new List<SelectListItem>
                            {
                                new SelectListItem() { Text = "", Value = "" }
                            };

        listTenantVariableModel.ForEach ( a =>
        {
            SelectListItem objItem = new SelectListItem
            {
                Text = a.Text.Trim ( ),
                Value = a.ValueID.ToString ( ).Trim ( )
            };

            objList.Add ( objItem );

        } );

        return objList.AsEnumerable ( );
    }

    //[ResponseCache ( CacheProfileName = "Cache1dayServerNBrowser" )]
    public static IEnumerable<SelectListItem> GetCategoryList ( EnumStoreType store )
    {
        return GetSelectList ( TenantStoreHelper.GetCategoryList ( store ) );
    }

    public static string GetCategoryText ( EnumStoreType store,int categoryId )
    {
        return TenantStoreHelper.GetTextForCategoryId ( categoryId,store );
    }

    public static string GetSubCategoryText ( EnumStoreType store,int subCategoryId )
    {
        return TenantStoreHelper.GetTextForSubCategoryId ( subCategoryId,store );
    }
}