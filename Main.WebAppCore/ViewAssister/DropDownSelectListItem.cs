
namespace Main.WebAppCore.ViewAssister;

public class SelectListItemDropDown
{
    public SelectListItemDropDown() { }
   
    public static IEnumerable<SelectListItem> GetHomeServiceAvailableList()
    {
        var listCountries = EnumListObjects.GetHomeServiceAvailableList().OrderBy(a => a.ValueID).ToList();
        List<SelectListItem> objOfferTypeListItems = new List<SelectListItem>();
        SelectListItem objItem = new SelectListItem();
        objItem.Text = "";
        objItem.Value = null;
        objOfferTypeListItems.Add(objItem);
        foreach (var item in listCountries)
        {
            objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString();
            objOfferTypeListItems.Add(objItem);
        }
        return objOfferTypeListItems.AsEnumerable();
    }

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetPartsPurchaseByList()
    {
        var listCountries = LocationRelatedSeed.GetPartsPurchaseByList().OrderBy(a => a.ValueID).ToList();
        List<SelectListItem> objOfferTypeListItems = new List<SelectListItem>();
        SelectListItem objItem = new SelectListItem();
        objItem.Text = "";
        objItem.Value = null;
        objOfferTypeListItems.Add(objItem);
        foreach (var item in listCountries)
        {
            objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString();
            objOfferTypeListItems.Add(objItem);
        }
        return objOfferTypeListItems.AsEnumerable();
    }

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetReportLengthList()
    {
        var listCountries = LocationRelatedSeed.GetReportLengthList().OrderBy(a => a.ValueID).ToList();
        List<SelectListItem> objOfferTypeListItems = new List<SelectListItem>();
        SelectListItem objItem = new SelectListItem();
        objItem.Text = "";
        objItem.Value = null;
        objOfferTypeListItems.Add(objItem);
        foreach (var item in listCountries)
        {
            objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString();
            objOfferTypeListItems.Add(objItem);
        }
        return objOfferTypeListItems.AsEnumerable();
    }

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetStepsList()
    {
        var listCountries = LocationRelatedSeed.GetStepNumberList().OrderBy(a => a.Text).ToList();
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
    public static IEnumerable<SelectListItem> GetPaidByList()
    {
        var listCountries = LocationRelatedSeed.GetPaidByList().OrderBy(a => a.Text).ToList();
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
        var listCountries = LocationRelatedSeed.GetPostTypeList().OrderBy(a => a.Text).ToList();
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
    public static IEnumerable<SelectListItem> GetOfferTypeList()
    {
        var listCountries = LocationRelatedSeed.GetOfferTypeList().OrderBy(a => a.Text).ToList();
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
    public static IEnumerable<SelectListItem> GetPaymentTimeList()
    {
        CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
        var cultureNmae = currentCulture.Name;

        List<SelectListItem> objCurrencyListItems = new List<SelectListItem>();
        objCurrencyListItems.Add(new SelectListItem() { Value = null, Text = "" });

        SelectListItem objItem = new SelectListItem();
        objItem.Text = GlobalResources.Localizer["DDL_Item_MonthlyPayment"];
        objItem.Value = "1";
        objCurrencyListItems.Add(objItem);

        objItem = new SelectListItem();
        objItem.Text = GlobalResources.Localizer["DDL_Item_YearlyPayment"];
        objItem.Value = "2";
        objCurrencyListItems.Add(objItem);

        return objCurrencyListItems.AsEnumerable();
    }

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetPageList()
    {
        var listCountries = LocationRelatedSeed.GetPublicPages().OrderBy(a => a.Text).ToList();
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
    public static IEnumerable<SelectListItem> GetPanelDisplayPositionList()
    {
        var listColumns = LocationRelatedSeed.GetPanelDisplayPositionList().OrderBy(a => a.Text).ToList();
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
        return objCurrencyListItems.AsEnumerable();
    }

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetPanelTempletList()
    {
        var listColumns = LocationRelatedSeed.GetPanelTempletList().OrderBy(a => a.Text).ToList();
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
    public static IEnumerable<SelectListItem> GetImageCategoryList()
    {
        var listColumns = LocationRelatedSeed.GetImageCategories().OrderBy(a => a.Text).ToList();
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
        return objCurrencyListItems.AsEnumerable();
    }

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetColumnList()
    {
        var listColumns = LocationRelatedSeed.GetColumnList().OrderBy(a => a.ValueID).ToList();
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
        return objCurrencyListItems.AsEnumerable();
    }

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetColumnListMobile()
    {
        var listColumns = LocationRelatedSeed.GetColumnList().OrderBy(a => a.ValueID).ToList();
        List<SelectListItem> objCurrencyListItems = new List<SelectListItem>();
        foreach (var item in listColumns.Where(a => a.ValueID != 3 && a.ValueID != 4).ToList())
        {
            SelectListItem objItem = new SelectListItem
            {
                Text = item.Text,
                Value = item.ValueID.ToString()
            };
            objCurrencyListItems.Add(objItem);
        }
        return objCurrencyListItems.AsEnumerable();
    }

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetCurrencyList()
    {
        var listCurrency = LocationRelatedSeed.GetCurrencyList().OrderBy(a => a.Text).ToList();
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
        var listCountries = LocationRelatedSeed.GetCountryList().OrderBy(a => a.Text).ToList();
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

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetPackageTypeList()
    {
        var listTypes = LocationRelatedSeed.GetPackageTypeList().OrderBy(a => a.Text).ToList();
        List<SelectListItem> objTypeListItems = new List<SelectListItem>();
        objTypeListItems.Add(new SelectListItem() { Value = null, Text = "" });
        foreach (var item in listTypes)
        {
            SelectListItem objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString();
            objTypeListItems.Add(objItem);
        }
        return objTypeListItems.AsEnumerable();
    }

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetSubscriptionPeriodList()
    {
        var listTypes = LocationRelatedSeed.GetSubscriptionPeriodList().OrderBy(a => a.Text).ToList();
        List<SelectListItem> objTypeListItems = new List<SelectListItem>();
        objTypeListItems.Add(new SelectListItem() { Value = null, Text = "" });
        foreach (var item in listTypes)
        {
            SelectListItem objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString();
            objTypeListItems.Add(objItem);
        }
        return objTypeListItems.AsEnumerable();
    }

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetPackageStatusList()
    {
        var listStatuses = LocationRelatedSeed.GetPackageStatusList().OrderBy(a => a.Text).ToList();
        List<SelectListItem> objStatusListItems = new List<SelectListItem>();
        objStatusListItems.Add(new SelectListItem() { Value = null, Text = "" });
        foreach (var item in listStatuses)
        {
            SelectListItem objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString();
            objStatusListItems.Add(objItem);
        }
        return objStatusListItems.AsEnumerable();
    }

    [ResponseCache(CacheProfileName = "Cache30Mins")]
    public static IEnumerable<SelectListItem> GetCountryStateList(EnumCountry country, bool isAllCountry)
    {
        return GetAValueSelectList(LocationRelatedSeed.GetCountryStates(country, isAllCountry), "");
    }

    [ResponseCache(CacheProfileName = "Cache30Mins")]
    public static IEnumerable<SelectListItem> GetSubCategoryList(Int64 categoryId, EnumCategoryFor categoryFor)
    {
        var listSubCat = new List<AValueModel>();
       
        listSubCat = BusinessObjectSeed.GetCateSubCategoryListByVariableAndParent(EnumAllowedVariable.SubCategory, categoryId, categoryFor);
        return GetAValueSelectList(listSubCat, "");
    }

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetSubCategoryList(EnumCategoryFor categoryFor)
    {
        if (categoryFor == EnumCategoryFor.DeshiHutBazar)
        {
            var list = GetCateSubCategoryListByVariable(EnumAllowedVariable.SubCategory);
            return GetAValueSelectList(list, "").ToList().AsEnumerable();
        }
        else if (categoryFor == EnumCategoryFor.FineArts)
        {
            var list = FineArtsGetSubCategoryList();
            return GetAValueSelectList(list, "").ToList().AsEnumerable();
        }
        return Enumerable.Empty<SelectListItem>();
    }

    public static List<AValueModel> FineArtsCategorySubCategorySeed()
    {
        List<AValueModel> ListEnglishAValues = new List<AValueModel>()
        {
            new AValueModel { ValueID = 1, ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = "ARTS", IconLink= "fas fa-palette" },
            new AValueModel { ValueID = 2, ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = "CRAFTS", IconLink= "fas fa-cubes" },
            new AValueModel { ValueID = 3, ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = "COLLECTIBLES", IconLink= "fas fa-" }
            };

        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = 5,
            ParentValueID = 1,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Painting",
            IconLink = "fa fa-car-alt link-dark"
        });

        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = 6,
            ParentValueID = 1,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Sculpture",
            IconLink = "fa fa-car-alt link-dark"
        });

        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = 7,
            ParentValueID = 1,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Drawing",
            IconLink = "fa fa-car-alt link-dark"
        });

        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = 8,
            ParentValueID = 1,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Water Color",
            IconLink = "fa fa-car-alt link-dark"
        });

        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = 9,
            ParentValueID = 1,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Photography",
            IconLink = "fa fa-car-alt link-dark"
        });

        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = 9,
            ParentValueID = 1,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Photography",
            IconLink = "fa fa-car-alt link-dark"
        });

        //Crafts
        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = 10,
            ParentValueID = 2,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Jute",
            IconLink = "fa fa-cubes link-dark"
        });

        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = 11,
            ParentValueID = 2,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Handicrafts",
            IconLink = "fa fa-cubes link-dark"
        });

        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = 12,
            ParentValueID = 2,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Metal",
            IconLink = "fa fa-cubes link-dark"
        });


        //Collectibles
        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = 13,
            ParentValueID = 3,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Stationery",
            IconLink = "fa fa-pen link-dark"
        });

        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = 14,
            ParentValueID = 3,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Coins & Currency",
            IconLink = "fa fa-pen link-dark"
        });

        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = 15,
            ParentValueID = 3,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Stamps",
            IconLink = "fa fa-pen link-dark"
        });

        return ListEnglishAValues;
    }

    public static List<AValueModel> FineArtsGetSubCategoryList()
    {
        var cateSubCategorySeedDataList = FineArtsCategorySubCategorySeed();
        return cateSubCategorySeedDataList
                            .ToList()
                            .Where(a => a.Variable == EnumAllowedVariable.SubCategory)
                            .ToList<AValueModel>();
    }

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetDeviceTypeList()
    {
        var listDeviceTypes = LocationRelatedSeed.GetDeviceTypeList();
        List<SelectListItem> objListItems = new List<SelectListItem>();
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
        var listShowHideList = LocationRelatedSeed.GetShowHideList();
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

    private static IEnumerable<SelectListItem> GetAValueSelectList(List<AValueModel> listAValue, string selectText)
    {
        List<SelectListItem> objList = new List<SelectListItem>() { new SelectListItem() { Text = selectText, Value = null, Selected = true } };
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

    public static IEnumerable<SelectListItem> GetAValueSelectList(List<AValue> listAValue)
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

    public static IEnumerable<SelectListItem> GetAValueSelectList(List<AValueModel> listAValue)
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
        var listStates = LocationRelatedSeed.GetCountryStates(EnumCountry.Bangladesh, false);
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

    public static List<AValueModel> GetCateSubCategoryAValueEnglishSeed()
    {
        List<AValueModel> ListEnglishAValues = new List<AValueModel>()
        {
            //category
            new AValueModel() { ValueID = (long) EnumMarket.Electronics, ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["Electronics"], IconLink= "fas fa-tablet-alt" },
            new AValueModel() { ValueID = (long) EnumMarket.TVHomeAppliances , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["TVHomeAppliances"].Value, IconLink= "fas fa-video" },
            new AValueModel() { ValueID = (long) EnumMarket.HomeAndLiving , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["HomeAndLivings"], IconLink= "fas fa-store-alt" },
            new AValueModel() { ValueID = (long) EnumMarket.Beauty , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["Beauty"], IconLink= "fas fa-user-injured" },
            new AValueModel() { ValueID = (long) EnumMarket.Health , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["Health"], IconLink= "fas fa-briefcase-medical" },
            new AValueModel() { ValueID = (long) EnumMarket.WomanFashion , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["WomanFashion"] , IconLink= "fas fa-venus-double" },
            new AValueModel() { ValueID = (long) EnumMarket.MenFashion , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["MenFashion"] , IconLink= "fas fa-user" },
            new AValueModel() { ValueID = (long) EnumMarket.ToysKidsAndBabies , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["ToysKidsAndBabies"] , IconLink= "fas fa-child" },
            new AValueModel() { ValueID = (long) EnumMarket.FitnessAndLifeStyles , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["FitnessAndLifeStyles"] , IconLink= "fa fa-baseball-ball" },
            new AValueModel() { ValueID = (long) EnumMarket.TrainingHireTravels , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["TrainingHireTravels"] , IconLink= "fa fa-university" },
            new AValueModel() { ValueID = (long) EnumMarket.ExportImports , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["ExportImport"]  , IconLink= "fas fa-warehouse" },
            new AValueModel() { ValueID = (long) EnumMarket.Motor , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["Motor"]  , IconLink= "fas fa-motorcycle" },
            new AValueModel() { ValueID = (long) EnumMarket.GroceriesAndPets , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["Groceries"]  , IconLink= "fas fa-cookie-bite" },
            new AValueModel() { ValueID = (long) EnumMarket.Others , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["Other"]  , IconLink= "fa fa-random" }
        };

        //category ids for parent of subcategory
        var electronics = (long)EnumMarket.Electronics;
        var tvhomeappliances = (long)EnumMarket.TVHomeAppliances;
        var homeliving = (long)EnumMarket.HomeAndLiving;
        var beauty = (long)EnumMarket.Beauty;
        var health = (long)EnumMarket.Health;
        var womenfashion = (long)EnumMarket.WomanFashion;
        var menfashion = (long)EnumMarket.MenFashion;
        var toyskidsandbabies = (long)EnumMarket.ToysKidsAndBabies;
        var fitnessandlifestyle = (long)EnumMarket.FitnessAndLifeStyles;
        var exportimports = (long)EnumMarket.ExportImports;
        var traininghiretravels = (long)EnumMarket.TrainingHireTravels;
        var motor = (long)EnumMarket.Motor;
        var groceries = (long)EnumMarket.GroceriesAndPets;
        var other = (long)EnumMarket.Others;

        //Electronics = 1001,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1001001
            ValueID = (long)EnumSpecialMarket.AudioAndSoundSystems,
            ParentValueID = electronics,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["AudioAndSoundSystems"],
            IconLink = "fa fa-car-alt"
        });
        //Vehicle = 1001,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1001002
            ValueID = (long)EnumSpecialMarket.LaptopAndPCs,
            ParentValueID = electronics,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["LaptopAndComputer"],
            IconLink = "fa fa-car-alt"
        });
        //Vehicle = 1001,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1001003
            ValueID = (long)EnumSpecialMarket.CameraAndAccesories,
            ParentValueID = electronics,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["CameraAndAccessories"],
            IconLink = "fa fa-car-alt"
        });
        //Vehicle = 1001,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1001004
            ValueID = (long)EnumSpecialMarket.MobilePhones,
            ParentValueID = electronics,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MobilePhone"],
            IconLink = "fa fa-car-alt"
        });
        //Vehicle = 1001,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1001005
            ValueID = (long)EnumSpecialMarket.TabletAndGadgets,
            ParentValueID = electronics,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["TabletAndGadget"],
            IconLink = "fa fa-car-alt"
        });
        //Vehicle = 1001,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1001006
            ValueID = (long)EnumSpecialMarket.PhoneAccessories,
            ParentValueID = electronics,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["PhoneAccessories"],
            IconLink = "fa fa-car-alt"
        });

        //TV & Home Appliances = 1002,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1002001
            ValueID = (long)EnumSpecialMarket.KitchenAppliances,
            ParentValueID = tvhomeappliances,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["KitchenAppliances"],
            IconLink = "fa fa fa-home"
        });

        //TV & Home Appliances = 1002,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID =  1002002
            ValueID = (long)EnumSpecialMarket.TVAndHomeAudio,
            ParentValueID = tvhomeappliances,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["TVAndHomeAudio"],
            IconLink = "fa fa fa-home"
        });

        //TV & Home Appliances = 1002,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID =  1002003
            ValueID = (long)EnumSpecialMarket.VacuumAndFloorCare,
            ParentValueID = tvhomeappliances,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["VacuumAndFloorCare"],
            IconLink = "fa fa fa-home"
        });

        //TV & Home Appliances = 1002,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID =  1002004
            ValueID = (long)EnumSpecialMarket.CoolingAndHeating,
            ParentValueID = tvhomeappliances,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["CoolingAndHeating"],
            IconLink = "fa fa fa-home"
        });

        //TV & Home Appliances = 1002,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID =  1002005
            ValueID = (long)EnumSpecialMarket.LargeAppliances,
            ParentValueID = tvhomeappliances,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["LargeAppliances"],
            IconLink = "fa fa fa-home"
        });

        //Home & Living = 1003,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1003001
            ValueID = (long)EnumSpecialMarket.DiyAndCleaning,
            ParentValueID = homeliving,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["DiyAndCleaning"],
            IconLink = "fa fa fa-home"
        });

        //Home & Living = 1003,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1003002
            ValueID = (long)EnumSpecialMarket.KitchenAndDining,
            ParentValueID = homeliving,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["KitchenAndDining"],
            IconLink = "fa fa fa-home"
        });


        //Home & Living = 1003,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1003003
            ValueID = (long)EnumSpecialMarket.Furniture,
            ParentValueID = homeliving,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Furniture"],
            IconLink = "fa fa fa-home"
        });


        //Home & Living = 1003,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1003003
            ValueID = (long)EnumSpecialMarket.OfficeFurniture,
            ParentValueID = homeliving,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["OfficeFurniture"],
            IconLink = "fa fa fa-home"
        });


        //Home & Living = 1003,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1003004
            ValueID = (long)EnumSpecialMarket.BathBedAndDecor,
            ParentValueID = homeliving,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["BathBedAndDecor"],
            IconLink = "fa fa fa-home"
        });

        //////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////

        //Beauty = 1004,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1004001
            ValueID = (long)EnumSpecialMarket.Makeup,
            ParentValueID = beauty,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Makeup"],
            IconLink = "fa fa fa-home"
        });

        //Beauty = 1004,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1004002
            ValueID = (long)EnumSpecialMarket.SkinCare,
            ParentValueID = beauty,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["SkinCare"],
            IconLink = "fa fa fa-home"
        });

        //Beauty = 1004,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1004003
            ValueID = (long)EnumSpecialMarket.HairBathBody,
            ParentValueID = beauty,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["HairBathBody"],
            IconLink = "fa fa fa-home"
        });

        //Beauty = 1004,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1004004
            ValueID = (long)EnumSpecialMarket.BeautyTools,
            ParentValueID = beauty,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["BeautyTools"],
            IconLink = "fa fa fa-home"
        });

        //////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////

        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = (long)EnumSpecialMarket.Wellbeings,
            ParentValueID = health,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Wellbeings"],
            IconLink = "fa fa fa-laptop"
        });

        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = (long)EnumSpecialMarket.PharmacyProducts,
            ParentValueID = health,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Pharmacy"],
            IconLink = "fa fa fa-laptop"
        });


        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = (long)EnumSpecialMarket.MedicalSupplies,
            ParentValueID = health,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MedicalSupplies"],
            IconLink = "fa fa fa-laptop"
        });

        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = (long)EnumSpecialMarket.BeautySupplements,
            ParentValueID = health,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["BeautySupplements"],
            IconLink = "fa fa fa-laptop"
        });


        ListEnglishAValues.Add(new AValueModel()
        {
            ValueID = (long)EnumSpecialMarket.PersonalCare,
            ParentValueID = health,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["PersonalCare"],
            IconLink = "fa fa fa-laptop"
        });


        //HomeAppliances = 1006,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1006001
            ValueID = (long)EnumSpecialMarket.WomenBags,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomenBags"],
            IconLink = "fa fa fa-laptop"
        });

        //HomeAppliances = 1006,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1006002
            ValueID = (long)EnumSpecialMarket.WomenGolds,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomenGolds"],
            IconLink = "fa fa fa-laptop"
        });


        //HomeAppliances = 1006,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1006003
            ValueID = (long)EnumSpecialMarket.WomenDresses,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomenDresses"],
            IconLink = "fa fa fa-laptop"
        });


        //HomeAppliances = 1006,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1006004
            ValueID = (long)EnumSpecialMarket.WomenShoes,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MenShoes"],
            IconLink = "fa fa fa-laptop"
        });


        //HomeAppliances = 1006,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1006005
            ValueID = (long)EnumSpecialMarket.WomenSpecticals,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Specticals"],
            IconLink = "fa fa fa-laptop"
        });


        //HomeAppliances = 1006,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1006006
            ValueID = (long)EnumSpecialMarket.WomenWatches,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomenWatches"],
            IconLink = "fa fa fa-laptop"
        });

        //HomeAppliances = 1006,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1006006
            ValueID = (long)EnumSpecialMarket.WomenFashionAccessories,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomenFashionAccessories"],
            IconLink = "fa fa fa-laptop"
        });

        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1006006
            ValueID = (long)EnumSpecialMarket.WomenBoishakDress,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomenFashionWomenBoishakDress"],
            IconLink = "fa fa fa-laptop"
        });


        //WomanDress = 1007,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1007001
            ValueID = (long)EnumSpecialMarket.MenWaletBags,
            ParentValueID = menfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MenWaletBags"],
            IconLink = "fa fa fa-laptop"
        });

        //WomanDress = 1007,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1007002
            ValueID = (long)EnumSpecialMarket.MenWatches,
            ParentValueID = menfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MenWatches"],
            IconLink = "fa fa fa-laptop"
        });

        //WomanDress = 1007,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1007003
            ValueID = (long)EnumSpecialMarket.MenDresses,
            ParentValueID = menfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MenDresses"],
            IconLink = "fa fa fa-laptop"
        });

        //WomanDress = 1007,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1007004
            ValueID = (long)EnumSpecialMarket.MenShoes,
            ParentValueID = menfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MenShoes"],
            IconLink = "fa fa fa-laptop"
        });

        //WomanDress = 1007,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1007005
            ValueID = (long)EnumSpecialMarket.Specticals,
            ParentValueID = menfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Specticals"],
            IconLink = "fa fa fa-laptop"
        });

        //WomanDress = 1007,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1007005
            ValueID = (long)EnumSpecialMarket.MenFashionAccessories,
            ParentValueID = menfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MenFashionAccessories"],
            IconLink = "fa fa fa-laptop"
        });

        //////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////

        //MenFashion = 1008,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1008001
            ValueID = (long)EnumSpecialMarket.BabyMaternity,
            ParentValueID = toyskidsandbabies,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["BabyMaternity"],
            IconLink = "fa fa fa-laptop"
        });

        //MenFashion = 1008,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1008002
            ValueID = (long)EnumSpecialMarket.BabyGears,
            ParentValueID = toyskidsandbabies,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["BabyGears"],
            IconLink = "fa fa fa-laptop"
        });

        //MenFashion = 1008,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1008003
            ValueID = (long)EnumSpecialMarket.BabyDiapers,
            ParentValueID = toyskidsandbabies,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["BabyDiapers"],
            IconLink = "fa fa fa-laptop"
        });

        //MenFashion = 1008,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1008004
            ValueID = (long)EnumSpecialMarket.ToysGames,
            ParentValueID = toyskidsandbabies,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["ToysGames"],
            IconLink = "fa fa fa-laptop"
        });
        //////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////

        //ChildrenAndEssentials = 1009,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1009001
            ValueID = (long)EnumSpecialMarket.CarOilFluids,
            ParentValueID = motor,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["CarOilFluids"],
            IconLink = "fa fa fa-laptop"
        });

        //ChildrenAndEssentials = 1009,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1009002
            ValueID = (long)EnumSpecialMarket.CarEssentials,
            ParentValueID = motor,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["CarEssentials"],
            IconLink = "fa fa fa-laptop"
        });

        //ChildrenAndEssentials = 1009,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1009003
            ValueID = (long)EnumSpecialMarket.MotorCycleEssentials,
            ParentValueID = motor,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MotorCycleEssentials"],
            IconLink = "fa fa fa-laptop"
        });

        //ChildrenAndEssentials = 1009,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1009004
            ValueID = (long)EnumSpecialMarket.CarServicesInstallations,
            ParentValueID = motor,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["CarServicesInstallations"],
            IconLink = "fa fa fa-laptop"
        });

        //////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////

        //HealthAndBeauty = 1010,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1010001
            ValueID = (long)EnumSpecialMarket.MensSportswear,
            ParentValueID = fitnessandlifestyle,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MensSportswear"],
            IconLink = "fa fa fa-laptop"
        });

        //HealthAndBeauty = 1010,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1010002
            ValueID = (long)EnumSpecialMarket.WomensSportswear,
            ParentValueID = fitnessandlifestyle,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomensSportswear"],
            IconLink = "fa fa fa-laptop"
        });

        //HealthAndBeauty = 1010,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1010003
            ValueID = (long)EnumSpecialMarket.FitnessEquipments,
            ParentValueID = fitnessandlifestyle,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["FitnessEquipments"],
            IconLink = "fa fa fa-laptop"
        });

        //HealthAndBeauty = 1010,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1010004
            ValueID = (long)EnumSpecialMarket.GlobalFittness,
            ParentValueID = fitnessandlifestyle,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["GlobalFittness"],
            IconLink = "fa fa fa-laptop"
        });

        //HealthAndBeauty = 1010,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1010005
            ValueID = (long)EnumSpecialMarket.MusicBooksGames,
            ParentValueID = fitnessandlifestyle,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MusicBooksGames"],
            IconLink = "fa fa fa-laptop"
        });



        //////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////

        //HomeAndLiving = 1011,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1011001
            ValueID = (long)EnumSpecialMarket.SnacksCookies,
            ParentValueID = groceries,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["SnacksCookies"],
            IconLink = "fa fa fa-laptop"
        });

        //HomeAndLiving = 1011,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1011002
            ValueID = (long)EnumSpecialMarket.Nuts,
            ParentValueID = groceries,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Nuts"],
            IconLink = "fa fa fa-laptop"
        });

        //HomeAndLiving = 1011,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1011003
            ValueID = (long)EnumSpecialMarket.Beverages,
            ParentValueID = groceries,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Beverages"],
            IconLink = "fa fa fa-laptop"
        });

        //HomeAndLiving = 1011,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1011004
            ValueID = (long)EnumSpecialMarket.GroceryEssentials,
            ParentValueID = groceries,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["GroceryEssentials"],
            IconLink = "fa fa fa-laptop"
        });

        //HomeAndLiving = 1011,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1011005
            ValueID = (long)EnumSpecialMarket.Laundry,
            ParentValueID = groceries,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Laundry"],
            IconLink = "fa fa fa-laptop"
        });

        //HomeAndLiving = 1011,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1011005
            ValueID = (long)EnumSpecialMarket.Pets,
            ParentValueID = groceries,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Pets"],
            IconLink = "fa fa fa-laptop"
        });

        //////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////

        //ForOffice = 1012,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1012001
            ValueID = (long)EnumSpecialMarket.TrainingPackages,
            ParentValueID = traininghiretravels,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["TrainingPackages"],
            IconLink = "fa fa fa-laptop"
        });

        //ForOffice = 1012,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1012002
            ValueID = (long)EnumSpecialMarket.TravelPackages,
            ParentValueID = traininghiretravels,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["TravelPackages"],
            IconLink = "fa fa fa-laptop"
        });

        //ForOffice = 1012,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1012003
            ValueID = (long)EnumSpecialMarket.HireMe,
            ParentValueID = traininghiretravels,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["HireMe"],
            IconLink = "fa fa fa-laptop"
        });


        //////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////

        //SportsAndHobbies = 1013,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1013001
            ValueID = (long)EnumSpecialMarket.ExportProducts,
            ParentValueID = exportimports,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["ExportProducts"],
            IconLink = "fa fa fa-laptop"
        });

        //SportsAndHobbies = 1013,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1013002
            ValueID = (long)EnumSpecialMarket.ImportProducts,
            ParentValueID = exportimports,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["ImportProducts"],
            IconLink = "fa fa fa-laptop"
        });

        ////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////
        ///


        //SportsAndHobbies = 1014,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1014002
            ValueID = (long)EnumSpecialMarket.OtherProductItems,
            ParentValueID = other,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["OtherProductItems"],
            IconLink = "fa fa fa-laptop"
        });

        //SportsAndHobbies = 1014,
        ListEnglishAValues.Add(new AValueModel()
        {
            //ValueID = 1014002
            ValueID = (long)EnumSpecialMarket.OtherServiceItems,
            ParentValueID = other,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["OtherServiceItems"],
            IconLink = "fa fa fa-laptop"
        });


        return ListEnglishAValues;
    }

    public static List<AValueModel> GetCateSubCategoryListByVariable(EnumAllowedVariable variable)
    {
        var cateSubCategorySeedDataList = GetCateSubCategoryAValueEnglishSeed();
        return cateSubCategorySeedDataList.Where(a => a.Variable == variable).OrderBy(order => order.ValueID).ToList();
    }


    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetCategoryList(EnumCategoryFor categoryFor)
    {
        if (categoryFor == EnumCategoryFor.DeshiHutBazar)
        {
            return GetAValueSelectList(GetCateSubCategoryListByVariable(EnumAllowedVariable.Category), "").ToList().AsEnumerable();
        }
        else if (categoryFor == EnumCategoryFor.FineArts)
        {
            var list = FineArtsCategoryList();
            return GetAValueSelectList(list, "").ToList().AsEnumerable();
        }
        return Enumerable.Empty<SelectListItem>();
    }

    public static List<AValueModel> FineArtsCategoryList()
    {
        var cateSubCategorySeedDataList = FineArtsCategorySubCategorySeed();
        return cateSubCategorySeedDataList
                            .ToList()
                            .Where(a => a.Variable == EnumAllowedVariable.Category)
                            .ToList<AValueModel>();
    }

    [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
    public static IEnumerable<SelectListItem> GetCompanyList()
    {
        var listCountries = LocationRelatedSeed.GetCompanyList().OrderBy(a => a.Text).ToList();
        List<SelectListItem> objCoutryListItems = new List<SelectListItem>();
        objCoutryListItems.Add(new SelectListItem() { Value = "", Text = "" });
        foreach (var item in listCountries)
        {
            SelectListItem objItem = new SelectListItem();
            objItem.Text = item.Text;
            objItem.Value = item.ValueID.ToString();
            objCoutryListItems.Add(objItem);
        }
        return objCoutryListItems.AsEnumerable();
    }

}