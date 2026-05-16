namespace Main.Common;

public static class EnumDescription
{
    public static string GetDescription<TEnum>(TEnum value) where TEnum : Enum
    {
        var type = typeof(TEnum);

        var memberInfo = type.GetMember(value.ToString());

        if (memberInfo.Length > 0)
        {
            var attributes = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return ((System.ComponentModel.DescriptionAttribute)attributes[0]).Description;
            }
        }

        return value.ToString();
    }
}

