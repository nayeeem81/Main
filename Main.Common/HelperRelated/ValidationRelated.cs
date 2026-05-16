using System.Text.RegularExpressions;

namespace Main.Common.HelperRelated;

public static class ValidationRelated
{
    public static bool IsValidEmail(string email)
    {
        var r = new Regex(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");
        return !string.IsNullOrEmpty(email) && r.IsMatch(email);
    }

    public static bool IsNumeric(string number)
    {
        foreach (var c in number)
        {
            if (!char.IsDigit(c))
                return false;
        }
        return true;
    }
}
