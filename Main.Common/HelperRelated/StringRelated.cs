using System.Text;

namespace Main.Common.HelperRelated;    

public static class StringRelated
{
    public static string GetUserNameFromEmail(string email)
    {
        email = email.Substring(0, email.IndexOf('@'));

        StringBuilder stringBuilder = new StringBuilder(email.Length);

        foreach (char c in email)
        {
            if (char.IsLetterOrDigit(c))
            {
                stringBuilder.Append(c);
            }
        }

        string result = stringBuilder.ToString();

        if (string.IsNullOrEmpty(result))
            return email;

        return result;      
    }
}
