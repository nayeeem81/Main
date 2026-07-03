using System.Text.RegularExpressions;

namespace Main.Common;

public static class ValidationRelated
{
    public static bool IsValidEmail (string email)
    {
        var r = new Regex(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");
        return !string.IsNullOrEmpty (email) && r.IsMatch (email);
    }


    public static bool IsNumeric (string number)
    {
        foreach ( var c in number )
        {
            if ( !char.IsDigit (c) )
                return false;
        }
        return true;
    }

    public static EnumIsValidTemplate IsValidTemplate (int countActual,EnumPanelTemplate enumPanelTemplate)
    {
        int enumPostCount = (int) GetPostCount (enumPanelTemplate);

        switch ( enumPanelTemplate )
        {

            case EnumPanelTemplate.AdminSingleBanner:
                if ( ( int ) enumPostCount == countActual )
                {
                    return EnumIsValidTemplate.ExactMatchValid;
                }
                else if ( ( int ) enumPostCount > countActual )
                {
                    return EnumIsValidTemplate.GreaterMatchValid;
                }
                break;
            case EnumPanelTemplate.AdminBannerCarousel:
                if ( ( int ) enumPostCount == countActual )
                {
                    return EnumIsValidTemplate.ExactMatchValid;
                }
                else if ( ( int ) enumPostCount > countActual )
                {
                    return EnumIsValidTemplate.GreaterMatchValid;
                }
                break;
            case EnumPanelTemplate.ProductDouble:
                if ( ( int ) enumPostCount == countActual )
                {
                    return EnumIsValidTemplate.ExactMatchValid;
                }
                else if ( ( int ) enumPostCount > countActual )
                {
                    return EnumIsValidTemplate.GreaterMatchValid;
                }
                break;

            case EnumPanelTemplate.ProductTriangle:
                if ( ( int ) enumPostCount == countActual )
                {
                    return EnumIsValidTemplate.ExactMatchValid;
                }
                else if ( ( int ) enumPostCount > countActual )
                {
                    return EnumIsValidTemplate.GreaterMatchValid;
                }
                break;
            case EnumPanelTemplate.ProductQuard:
                if ( ( int ) enumPostCount == countActual )
                {
                    return EnumIsValidTemplate.ExactMatchValid;
                }
                else if ( ( int ) enumPostCount > countActual )
                {
                    return EnumIsValidTemplate.GreaterMatchValid;
                }
                break;
            case EnumPanelTemplate.ProductSixer:
                if ( ( int ) enumPostCount == countActual )
                {
                    return EnumIsValidTemplate.ExactMatchValid;
                }
                else if ( ( int ) enumPostCount > countActual )
                {
                    return EnumIsValidTemplate.GreaterMatchValid;
                }
                break;
            default:
                return EnumIsValidTemplate.Invalid;
        }

        return EnumIsValidTemplate.Invalid;
    }

    public static int GetPostCount (EnumPanelTemplate panelTemplate)
    {
        if ( panelTemplate == EnumPanelTemplate.AdminSingleBanner )
        {
            return ( int ) EnumPostCount.PostCountOne;
        }
        else if ( panelTemplate == EnumPanelTemplate.AdminBannerCarousel )
        {
            return ( int ) EnumPostCount.PostCountFour;
        }
        else if ( panelTemplate == EnumPanelTemplate.ProductDouble )
        {
            return ( int ) EnumPostCount.PostCountTwo;
        }
        else if ( panelTemplate == EnumPanelTemplate.ProductTriangle )
        {
            return ( int ) EnumPostCount.PostCountThree;
        }
        else if ( panelTemplate == EnumPanelTemplate.ProductQuard )
        {
            return ( int ) EnumPostCount.PostCountFour;
        }
        else if ( panelTemplate == EnumPanelTemplate.ProductSixer )
        {
            return ( int ) EnumPostCount.PostCountSix;
        }

        return 0;
    }
}