using Main.Common.Model;
using System.ComponentModel;
using System.Reflection;

namespace Main.Common;

public class ListEnum
{
    private static readonly Dictionary<EnumCountry, EnumCurrency> objCountryCurrencyList = new Dictionary<EnumCountry, EnumCurrency>()
    {
        { EnumCountry.Bangladesh, EnumCurrency.BDT}
    };

    
    public static EnumCurrency GetCountryCurrency(EnumCountry countryName)
    {
        var item = objCountryCurrencyList[countryName];
        return item;
    }
  
    public static string GetCountryCurrencyDescription(EnumCountry countryName)
    {
        var currency = GetCountryCurrency(countryName);

        return GetCurrencyDescription(currency);
    }

    private static readonly List<KeyValuePair<EnumCountry, EnumState>> objCountryStateList = new List<KeyValuePair<EnumCountry, EnumState>>()
    {
        new KeyValuePair<EnumCountry, EnumState>( EnumCountry.Bangladesh, EnumState.Dhaka),

        new KeyValuePair<EnumCountry, EnumState>(EnumCountry.Bangladesh, EnumState.Chittagong),

        new KeyValuePair<EnumCountry, EnumState>( EnumCountry.Bangladesh, EnumState.Rajshahi),

        new KeyValuePair<EnumCountry, EnumState>( EnumCountry.Bangladesh, EnumState.Khulna),

        new KeyValuePair<EnumCountry, EnumState>( EnumCountry.Bangladesh, EnumState.Barishal),

        new KeyValuePair<EnumCountry, EnumState>( EnumCountry.Bangladesh, EnumState.Sylhet),

        new KeyValuePair<EnumCountry, EnumState>( EnumCountry.Bangladesh, EnumState.Maimenshing),

        new KeyValuePair<EnumCountry, EnumState>( EnumCountry.Bangladesh, EnumState.Rangpur)
   };

   public static List<ParentChildVriableModel> GetCountryStates(EnumCountry countryName, bool IsAllCountry)
   {
        List<KeyValuePair<EnumCountry, EnumState>> listStates;
        if (IsAllCountry)
        {
            listStates = objCountryStateList.ToList();
        }
        else
        {
            listStates = objCountryStateList.Where(a => a.Key == countryName).ToList();
        }
        List<ParentChildVriableModel> objStateList = new List<ParentChildVriableModel>();

        Type enumType = typeof(EnumState);

        foreach (var item in listStates)
        {
            var value = item.Value;

            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            ParentChildVriableModel objPair = new ParentChildVriableModel
            {
                ValueID = (int) value
            };

            if (descriptionAttribute != null)
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString();
            }

            objStateList.Add(objPair);
        }

        return objStateList;
    }

    public static string GetCurrencyDescription(EnumCurrency currencyEnum)
    {
        Type enumType = typeof(EnumCurrency);
        var enumValues = enumType.GetEnumValues();

        foreach (EnumCurrency value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();
            if (value == currencyEnum)
            {
                if (descriptionAttribute != null)
                {
                    return descriptionAttribute.Description;
                }
                else
                {
                    return value.ToString();
                }
            }
        }

        return "";
    }

    public static string GetCountryDescription(EnumCountry countryEnum)
    {
        Type enumType = typeof(EnumCountry);
        var enumValues = enumType.GetEnumValues();

        foreach (EnumCountry value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();
            if (value == countryEnum)
            {
                if (descriptionAttribute != null)
                {
                    return descriptionAttribute.Description;
                }
                else
                {
                    return value.ToString();
                }
            }
        }

        return "";
    }

    public static string GetStateDescription(EnumState? stateEnum)
    {
        if (!stateEnum.HasValue)
            return "";

        Type enumType = typeof(EnumState);

        var enumValues = enumType.GetEnumValues();

        foreach (EnumState value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute 
                       = memberInfo.GetCustomAttribute<DescriptionAttribute>();

            if (value == stateEnum.Value)
            {
                if (descriptionAttribute != null)
                {
                    return descriptionAttribute.Description;
                }
                else
                {
                    return value.ToString();
                }
            }
        } 

        return "";
    }


    public static List<ParentChildVriableModel> GetCountryList()
    {
        List<ParentChildVriableModel> objCountryList = new List<ParentChildVriableModel>();

        Type enumType = typeof(EnumCountry);
        var enumValues = enumType.GetEnumValues();

        foreach (EnumCountry value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            ParentChildVriableModel objPair = new ParentChildVriableModel
            {
                ValueID = (int) value
            };
            if (descriptionAttribute != null)
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString();
            }
            if (objPair.ValueID != 0)
            {
                objCountryList.Add(objPair);
            }
        }
        return objCountryList;
    }

    public static List<ParentChildVriableModel> GetCurrencyList()
    {
        List<ParentChildVriableModel> objCurrencyList = new List<ParentChildVriableModel>();

        Type enumType = typeof(EnumCurrency);
        var enumValues = enumType.GetEnumValues();

        foreach (EnumCurrency value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            ParentChildVriableModel objPair 
                = new ParentChildVriableModel
            {
                ValueID = (int)value
            };

            if (descriptionAttribute != null)
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString();
            }

            objCurrencyList.Add(objPair);

        }
        return objCurrencyList;
    }

    public static List<ParentChildVriableModel> GetDeviceTypeList()
    {
        List<ParentChildVriableModel> objPackageTypeList = new List<ParentChildVriableModel>();

        Type enumType = typeof(EnumDeviceType);
        var enumValues = enumType.GetEnumValues();

        foreach (EnumDeviceType value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            ParentChildVriableModel objPair = new ParentChildVriableModel
            {
                ValueID = (int)value
            };

            if (descriptionAttribute != null)
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString();
            }

            objPackageTypeList.Add(objPair);
        }

        return objPackageTypeList;
    }

    public static List<ParentChildVriableModel> GetShowHideList()
    {
        List<ParentChildVriableModel> objPackageTypeList = new List<ParentChildVriableModel>();

        Type enumType = typeof(EnumShowOrHide);
        var enumValues = enumType.GetEnumValues();

        foreach (EnumShowOrHide value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            ParentChildVriableModel objPair = new ParentChildVriableModel();

            objPair.ValueID = (int)value;

            if (descriptionAttribute != null)
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString();
            }
            objPackageTypeList.Add(objPair);
        }

        return objPackageTypeList;
    }

    public static List<ParentChildVriableModel> GetPanelTempletList()
    {
        List<ParentChildVriableModel> objColumnList = new List<ParentChildVriableModel>();

        Type enumType = typeof(EnumPanelTemplate);
        var enumValues = enumType.GetEnumValues();

        foreach (EnumPanelTemplate value in enumValues)
        {
            MemberInfo memberInfo = enumType.GetMember(value.ToString()).First();

            var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();

            ParentChildVriableModel objPair = new ParentChildVriableModel
            {
                ValueID = (int)value
            };

            if (descriptionAttribute != null)
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString();
            }

            objColumnList.Add(objPair);
        }

        return objColumnList;
    }

    public static List<ParentChildVriableModel> GetPublicPages()
    {
        List<ParentChildVriableModel> objColumnList = new List<ParentChildVriableModel>();

        Type enumType = typeof(EnumPublicPage);
        var enumValues = enumType.GetEnumValues();

        foreach (EnumPublicPage value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            ParentChildVriableModel objPair = new ParentChildVriableModel
            {
                ValueID = (int)value
            };

            if (descriptionAttribute != null)
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString();
            }

            objColumnList.Add(objPair);
        }

        return objColumnList;
    }

    public static List<ParentChildVriableModel> GetOfferTypeList()
    {
        List<ParentChildVriableModel> objCountryList = new List<ParentChildVriableModel>();

        Type enumType = typeof(EnumOfferType);
        var enumValues = enumType.GetEnumValues();

        foreach (EnumOfferType value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            ParentChildVriableModel objPair = new ParentChildVriableModel
            {
                ValueID = (int)value
            };

            if (descriptionAttribute != null)
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString();
            }

            if (objPair.ValueID != 0)
            {
                objCountryList.Add(objPair);
            }
        }

        return objCountryList;
    }

    public static List<ParentChildVriableModel> GetPostTypeList()
    {
        List<ParentChildVriableModel> objCountryList = new List<ParentChildVriableModel>();

        Type enumType = typeof(EnumPostType);
        var enumValues = enumType.GetEnumValues();

        foreach (EnumPostType value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            ParentChildVriableModel objPair = new ParentChildVriableModel
            {
                ValueID = (int)value
            };



            if (descriptionAttribute != null)
            {
                objPair.Text = (string)descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString();
            }

            if (objPair != null)
            {
                objCountryList.Add(objPair);
            }
        }

        return objCountryList;
    }

    public static List<ParentChildVriableModel> GetPaidByList()
    {
        List<ParentChildVriableModel> objCountryList = new List<ParentChildVriableModel>();

        Type enumType = typeof(EnumPaidBy);
        var enumValues = enumType.GetEnumValues();

        foreach (EnumPaidBy value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            ParentChildVriableModel objPair = new ParentChildVriableModel
            {
                ValueID = (int)value
            };
            if (descriptionAttribute != null)
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString();
            }
            if (objPair.ValueID != 0)
            {
                objCountryList.Add(objPair);
            }
        }
        return objCountryList;
    }

    
    public static List<ParentChildVriableModel> GetCompanyList()
    {
        List<ParentChildVriableModel> objCountryList = new List<ParentChildVriableModel>();

        Type enumType = typeof(EnumCompanyName);
        var enumValues = enumType.GetEnumValues();

        foreach (EnumCompanyName value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            ParentChildVriableModel objPair = new ParentChildVriableModel
            {
                ValueID = (int)value
            };
            if (descriptionAttribute != null)
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString();
            }
            if (objPair.ValueID != 0)
            {
                objCountryList.Add(objPair);
            }
        }
        return objCountryList;
    }

    public static string? GetPageDescription(EnumPublicPage? page)
    {
        Type enumType = typeof(EnumPublicPage);
        var enumValues = enumType.GetEnumValues();

        foreach (EnumPublicPage value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();
            if (value == page.Value)
            {
                if (descriptionAttribute != null)
                {
                    return descriptionAttribute.Description;
                }
                else
                {
                    return value.ToString();
                }
            }
        }

        return "";
    }

    public static string? GetCompanyDescription(EnumCompanyName company)
    {
        Type enumType = typeof(EnumCompanyName);

        var enumValues = enumType.GetEnumValues();

        foreach (EnumCompanyName value in enumValues)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
            memberInfo.GetCustomAttribute<DescriptionAttribute>();

            if (value == company)
            {
                if (descriptionAttribute != null)
                {
                    return descriptionAttribute.Description;
                }
                else
                {
                    return value.ToString();
                }
            }
        }

        return "";
    }
}
