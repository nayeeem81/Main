namespace Main.Common.HelperRelated;

public static class DateRelated
{        
    public static string GetDateString(DateTime date)
    {
        var day = date.Day.ToString();
        var month = date.Month;
        var year = date.Year.ToString();
        var dateString = $"{day}-{GetMonthString(month)}-{year}";
        return dateString;
    }

    private static string GetMonthString(int month)
    {
        if (month < 1 && month > 12)
            return "";


        if (month == 1)
            return "Jan";

        else if (month == 2)
            return "Feb";

        else if (month == 3)
            return "Mar";

        else if (month == 4)
            return "Apr";

        else if (month == 5)
            return "May";

        else if (month == 6)
            return "Jun";

        else if (month == 7)
            return "July";

        else if (month == 8)
            return "Aug";

        else if (month == 9)
            return "Sep";

        else if (month == 10)
            return "Oct";

        else if (month == 11)
            return "Nov";

        else if (month == 12)
            return "Dec";

        return "";
    }

    public static DateTime GetBangladeshCurrentDateTime()
    {
        var BnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
        var BaTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, BnTimeZone);
        return BaTime;
    }
}
