using System.Text;

namespace Main.Common;

public static class StringRelated
{
    public static string GetUserNameFromEmail (string email)
    {
        email = email.Substring (0,email.IndexOf ('@'));

        StringBuilder stringBuilder = new StringBuilder(email.Length);

        foreach ( char c in email )
        {
            if ( char.IsLetterOrDigit (c) )
            {
                stringBuilder.Append (c);
            }
        }

        string result = stringBuilder.ToString();

        if ( string.IsNullOrEmpty (result) )
            return email;

        return result;
    }

    public static string GetTrimmedRemovedSpaseString (string input)
    {
        input = input.Trim ();
        input = input.Replace (" ","");

        return input;
    }
}
