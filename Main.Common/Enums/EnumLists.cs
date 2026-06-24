using Main.Common.Model;

using System.ComponentModel;
using System.Reflection;

namespace Main.Common.Enums;

public class ListEnum
{
    private static readonly Dictionary<EnumCountry, EnumCurrency> objCountryCurrencyList = new Dictionary<EnumCountry, EnumCurrency>()
    {
        { EnumCountry.Bangladesh, EnumCurrency.BDT}
    };

    public static EnumCurrency GetCountryCurrency ( EnumCountry countryName )
    {
        var item = objCountryCurrencyList[countryName];
        return item;
    }

    public static string GetCountryCurrencyDescription ( EnumCountry countryName )
    {
        var currency = GetCountryCurrency(countryName);

        return GetCurrencyDescription ( currency );
    }

    public static string GetCurrencyDescription ( EnumCurrency currencyEnum )
    {
        Type enumType = typeof(EnumCurrency);
        var enumValues = enumType.GetEnumValues();

        foreach ( EnumCurrency value in enumValues )
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();
            if ( value == currencyEnum )
            {
                if ( descriptionAttribute != null )
                {
                    return descriptionAttribute.Description;
                }
                else
                {
                    return value.ToString ( );
                }
            }
        }

        return "";
    }

    public static string GetCountryDescription ( EnumCountry countryEnum )
    {
        Type enumType = typeof(EnumCountry);
        var enumValues = enumType.GetEnumValues();

        foreach ( EnumCountry value in enumValues )
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();
            if ( value == countryEnum )
            {
                if ( descriptionAttribute != null )
                {
                    return descriptionAttribute.Description;
                }
                else
                {
                    return value.ToString ( );
                }
            }
        }

        return "";
    }

    public static List<TenantVariableModel> GetCountryList ( )
    {
        List<TenantVariableModel> objCountryList = new List<TenantVariableModel>();

        Type enumType = typeof(EnumCountry);
        var enumValues = enumType.GetEnumValues();

        foreach ( EnumCountry value in enumValues )
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            TenantVariableModel objPair = new TenantVariableModel
            {
                ValueID = (int) value
            };
            if ( descriptionAttribute != null )
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString ( );
            }
            if ( objPair.ValueID != 0 )
            {
                objCountryList.Add ( objPair );
            }
        }
        return objCountryList;
    }

    public static List<TenantVariableModel> GetCurrencyList ( )
    {
        List<TenantVariableModel> objCurrencyList = new List<TenantVariableModel>();

        Type enumType = typeof(EnumCurrency);
        var enumValues = enumType.GetEnumValues();

        foreach ( EnumCurrency value in enumValues )
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            TenantVariableModel objPair
                = new TenantVariableModel
                {
                    ValueID = (int)value
                };

            if ( descriptionAttribute != null )
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString ( );
            }

            objCurrencyList.Add ( objPair );

        }
        return objCurrencyList;
    }

    public static List<TenantVariableModel> GetShowHideList ( )
    {
        List<TenantVariableModel> objPackageTypeList = new List<TenantVariableModel>();

        Type enumType = typeof(EnumShowOrHide);
        var enumValues = enumType.GetEnumValues();

        foreach ( EnumShowOrHide value in enumValues )
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            TenantVariableModel objPair = new TenantVariableModel();

            objPair.ValueID = ( int ) value;

            if ( descriptionAttribute != null )
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString ( );
            }
            objPackageTypeList.Add ( objPair );
        }

        return objPackageTypeList;
    }

    public static List<TenantVariableModel> GetPanelTempletList ( )
    {
        List<TenantVariableModel> objColumnList = new List<TenantVariableModel>();

        Type enumType = typeof(EnumPanelTemplate);
        var enumValues = enumType.GetEnumValues();

        foreach ( EnumPanelTemplate value in enumValues )
        {
            MemberInfo memberInfo = enumType.GetMember(value.ToString()).First();

            var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();

            TenantVariableModel objPair = new TenantVariableModel
            {
                ValueID = (int)value
            };

            if ( descriptionAttribute != null )
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString ( );
            }

            objColumnList.Add ( objPair );
        }

        return objColumnList;
    }

    public static List<TenantVariableModel> GetPublicPages ( )
    {
        List<TenantVariableModel> objColumnList = new List<TenantVariableModel>();

        Type enumType = typeof(EnumPublicPage);
        var enumValues = enumType.GetEnumValues();

        foreach ( EnumPublicPage value in enumValues )
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            TenantVariableModel objPair = new TenantVariableModel
            {
                ValueID = (int)value
            };

            if ( descriptionAttribute != null )
            {
                objPair.Text = descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString ( );
            }

            objColumnList.Add ( objPair );
        }

        return objColumnList;
    }
    public static List<TenantVariableModel> GetPostTypeList ( )
    {
        List<TenantVariableModel> objCountryList = new List<TenantVariableModel>();

        Type enumType = typeof(EnumPostType);
        var enumValues = enumType.GetEnumValues();

        foreach ( EnumPostType value in enumValues )
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            TenantVariableModel objPair = new TenantVariableModel
            {
                ValueID = (int)value
            };



            if ( descriptionAttribute != null )
            {
                objPair.Text = ( string ) descriptionAttribute.Description;
            }
            else
            {
                objPair.Text = value.ToString ( );
            }

            if ( objPair != null )
            {
                objCountryList.Add ( objPair );
            }
        }

        return objCountryList;
    }

    public static List<TenantVariableModel> GetAdminPostTypeList ( )
    {
        List<TenantVariableModel> objAdminPostTypeList = new List<TenantVariableModel>();

        Type enumType = typeof(EnumPostType);
        var enumValues = enumType.GetEnumValues();

        foreach ( EnumPostType value in enumValues )
        {
            if ( value == EnumPostType.Product )
            {
                continue;
            }

            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            TenantVariableModel objPair = new TenantVariableModel
            {
                ValueID = (int) value
            };

            if ( descriptionAttribute != null )
            {
                objPair.Text = ( string ) descriptionAttribute.Description;
            }

            if ( objPair != null && objPair.ValueID != 0 )
            {
                objAdminPostTypeList.Add ( objPair );
            }
        }

        return objAdminPostTypeList;
    }

    public static string GetPageDescription ( EnumPublicPage page )
    {
        Type enumType = typeof(EnumPublicPage);
        var enumValues = enumType.GetEnumValues();

        foreach ( EnumPublicPage value in enumValues )
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();
            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();
            if ( value == page )
            {
                if ( descriptionAttribute != null )
                {
                    return descriptionAttribute.Description;
                }
                else
                {
                    return value.ToString ( );
                }
            }
        }

        return " ";
    }
}
