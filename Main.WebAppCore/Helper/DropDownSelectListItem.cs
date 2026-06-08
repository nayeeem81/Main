using dotless.Core.Parser.Functions;

using Main.Common.Enums;
using Main.Common.Helper;
using Main.Common.HelperServices;
using Main.Common.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using ResourceLibrary.Resources;

using System.Globalization;

namespace WebAppCore.Helper;

public class SelectListItemDropDown
{
    public SelectListItemDropDown() { }


    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetPaidByList()
    {
        var listCountries = ListEnum.GetPaidByList().OrderBy(a => a.Text).ToList();
        List<SelectListItem> objOfferTypeListItems = new List<SelectListItem>();
        foreach (var item in listCountries)
        {
            SelectListItem objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString();
            objOfferTypeListItems.Add(objItem);
        }
        return objOfferTypeListItems.AsEnumerable();
    }


    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetPostTypeList()
    {
        var listCountries = ListEnum.GetPostTypeList().OrderBy(a => a.Text).ToList();
        List<SelectListItem> objOfferTypeListItems = new List<SelectListItem>();
        foreach (var item in listCountries)
        {
            SelectListItem objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString();
            objOfferTypeListItems.Add(objItem);
        }
        return objOfferTypeListItems.AsEnumerable();
    }

    [ResponseCache ( CacheProfileName = "Cache1dayServerNBrowser" )]
    public static IEnumerable<SelectListItem> GetAdminPostTypeList ( )
    {
        var listCountries = ListEnum.GetAdminPostTypeList().ToList();

        List<SelectListItem> objOfferTypeListItems = new List<SelectListItem>();
        SelectListItem objItem;

        foreach ( var item in listCountries )
        {
            objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString ( );
            objOfferTypeListItems.Add ( objItem );
        }

        return objOfferTypeListItems.AsEnumerable ( );
    }


    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetOfferTypeList()
    {
        var listCountries = ListEnum.GetOfferTypeList().OrderBy(a => a.Text).ToList();
        List<SelectListItem> objOfferTypeListItems = new List<SelectListItem>();
        foreach (var item in listCountries)
        {
            SelectListItem objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString();
            objOfferTypeListItems.Add(objItem);
        }
        return objOfferTypeListItems.AsEnumerable();
    }


    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetPageList()
    {
        var listCountries = ListEnum.GetPublicPages().OrderBy(a => a.Text).ToList();
        List<SelectListItem> objCoutryListItems = new List<SelectListItem>();
        foreach (var item in listCountries)
        {
            SelectListItem objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString();
            objCoutryListItems.Add(objItem);
        }
        return objCoutryListItems.AsEnumerable();
    }


    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetPanelTempletList()
    {
        var listColumns = ListEnum.GetPanelTempletList().OrderBy(a => a.Text).ToList();

        List<SelectListItem> objCurrencyListItems = new List<SelectListItem>();

        foreach (var item in listColumns)
        {
            SelectListItem objItem = new SelectListItem
            {
                Text = item.Text,
                Value = item.ValueID.ToString()
            };
            objCurrencyListItems.Add(objItem);
        }

        SelectListItem objItem1 = new SelectListItem
        {
            Text = "",
            Value = ""
        };

        objCurrencyListItems.Add ( objItem1 );

        return objCurrencyListItems.AsEnumerable();
    }


    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetCurrencyList()
    {
        var listCurrency = ListEnum.GetCurrencyList().OrderBy(a => a.Text).ToList();

        List<SelectListItem> objCurrencyListItems = new List<SelectListItem>();

        objCurrencyListItems.Add(new SelectListItem() { Value = null, Text = "" });

        foreach (var item in listCurrency)
        {
            SelectListItem objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString();
            objCurrencyListItems.Add(objItem);
        }

        return objCurrencyListItems.AsEnumerable();
    }


    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetCountryList()
    {
        var listCountries = ListEnum.GetCountryList().OrderBy(a => a.Text).ToList();

        List<SelectListItem> objCoutryListItems = new List<SelectListItem>();

        objCoutryListItems.Add(new SelectListItem() { Value = "1", Text = "" });

        foreach (var item in listCountries)
        {
            SelectListItem objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString();
            objCoutryListItems.Add(objItem);
        }

        return objCoutryListItems.AsEnumerable();
    }


    [ResponseCache(CacheProfileName = "Cache30Mins")]
    public static IEnumerable<SelectListItem> GetCountryStateList(EnumCountry country, bool isAllCountry)
    {
        return GetSelectList(ListEnum.GetCountryStates(country, isAllCountry), "");
    }


    [ResponseCache(CacheProfileName = "Cache30Mins")]
    public static IEnumerable<SelectListItem> GetSubCategoryList(EnumCategoryFor categoryFor)
    {
        var listSubCat = new List<ParentChildVriableModel>();

        if ( categoryFor == EnumCategoryFor.FineArts )
        {
            listSubCat = BusinessSeedFineArts
                            .GetSubCategoryList ( );
        }
        else if ( categoryFor == EnumCategoryFor.LifeStyles )
        {
            listSubCat = BusinessSeedLifeStyle
                            .GetSubCategoryList ( );
        }
        
        return GetSelectList(listSubCat, "");
    }


    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetDeviceTypeList()
    {
        var listDeviceTypes = ListEnum.GetDeviceTypeList();

        List<SelectListItem> objListItems 
            = new List<SelectListItem>();

        listDeviceTypes.ForEach(a =>
        {
            SelectListItem objItem = new SelectListItem
            {
                Text = a.Text.Trim(),
                Value = a.ValueID.ToString().Trim()
            };
            objListItems.Add(objItem);
        });

        return objListItems.AsEnumerable();
    }


    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetShowHideList()
    {
        var listShowHideList = ListEnum.GetShowHideList();
        List<SelectListItem> objListItems = new List<SelectListItem>();
        listShowHideList.ForEach(a =>
        {
            SelectListItem objItem = new SelectListItem
            {
                Text = a.Text.Trim(),
                Value = a.ValueID.ToString().Trim()
            };
            objListItems.Add(objItem);
        });
        return objListItems.AsEnumerable();
    }


    private static IEnumerable<SelectListItem> GetSelectList(List<ParentChildVriableModel> listAValue, string selectText)
    {
        List<SelectListItem> objList = 
            new List<SelectListItem>() 
            { 
                new SelectListItem() { 
                    Text = selectText, 
                    Value = null, 
                    Selected = true 
                } };

        listAValue.ForEach(a =>
        {
            SelectListItem objItem = new SelectListItem
            {
                Text = a.Text.Trim(),
                Value = a.ValueID.ToString().Trim()
            };

            objList.Add(objItem);
        });

        return objList.AsEnumerable();
    }


    public static IEnumerable<SelectListItem> getSelectList(List<ParentChildVriableModel> listAValue)
    {
        List<SelectListItem> objList = new List<SelectListItem> { new SelectListItem() { Text = "", Value = "" } };
        listAValue.ForEach(a =>
        {
            SelectListItem objItem = new SelectListItem();
            objItem.Text = a.Text.Trim();
            objItem.Value = a.ValueID.ToString().Trim();
            objList.Add(objItem);
        });
        return objList.AsEnumerable();
    }


    public static IEnumerable<SelectListItem> GetSelectList ( List<ParentChildVriableModel> listAValue )
    {
        List<SelectListItem> objList = new List<SelectListItem> { new SelectListItem() { Text = "", Value = "" } };
        listAValue.ForEach ( a =>
        {
            SelectListItem objItem = new SelectListItem();
            objItem.Text = a.Text.Trim ( );
            objItem.Value = a.ValueID.ToString ( ).Trim ( );
            objList.Add ( objItem );
        } );
        return objList.AsEnumerable ( );
    }


    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static List<SelectListItem> GetAllContactMessageType()
    {
        CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
        var cultureNmae = currentCulture.Name;
        List<SelectListItem> objList = new List<SelectListItem>
        {
            new SelectListItem() { Text = "", Value = "", Selected = true },
            new SelectListItem() { Text = GlobalResources.Localizer["MessageGeneral"], Value = "1" },
            new SelectListItem() { Text = GlobalResources.Localizer["MessageTechnical"], Value = "2" },
            new SelectListItem() { Text = GlobalResources.Localizer["MessageAffiliateProgram"], Value = "3" },
            new SelectListItem() { Text = GlobalResources.Localizer["MessageFeaturedAds"], Value = "4" },
            new SelectListItem() { Text = GlobalResources.Localizer["MessageBilling"], Value = "5" },
            new SelectListItem() { Text = GlobalResources.Localizer["MessageBuyAdSpace"], Value = "6" }
        };
        return objList;
    }


    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static List<SelectListItem> GetAllStateList()
    {
        var listStates = ListEnum.GetCountryStates(EnumCountry.Bangladesh, false);
        List<SelectListItem> objList = new List<SelectListItem> { new SelectListItem() { Text = "", Value = "" } };
        listStates.ForEach(a =>
        {
            SelectListItem objItem = new SelectListItem();
            objItem.Text = a.Text.Trim();
            objItem.Value = a.ValueID.ToString().Trim();
            objList.Add(objItem);
        });
        return objList;
    }


    [ResponseCache ( CacheProfileName = "Cache1dayServerNBrowser" )]
    public static IEnumerable<SelectListItem> GetCategoryList ( EnumCategoryFor categoryFor )
    {
        if ( categoryFor == EnumCategoryFor.LifeStyles )
        {
            return GetSelectList
            (
                BusinessSeedLifeStyle.GetListByVariable ( EnumAllowedVariable.Category,EnumCategoryFor.LifeStyles )
            ,"" )
            .ToList ( ).AsEnumerable ( );
        }
        else if ( categoryFor == EnumCategoryFor.FineArts )
        {
            return GetSelectList
             (
                 BusinessSeedFineArts.GetListByVariable ( EnumAllowedVariable.Category,
                 EnumCategoryFor.LifeStyles )
             ,"" )
             .ToList ( ).AsEnumerable ( );
        }

        return new List<SelectListItem> ( );
    }
}