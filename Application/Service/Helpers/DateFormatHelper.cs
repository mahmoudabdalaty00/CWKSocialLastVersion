using System.Globalization;


namespace Application.Service.Helpers;


public static class DateFormatHelper
{
    private static readonly CultureInfo ArabicCulture = new CultureInfo("ar-EG");
    private static readonly CultureInfo EnglishCulture = CultureInfo.InvariantCulture;

    private static readonly Dictionary<char, char> ArabicNumerals = new Dictionary<char, char>
    {
        {'0', '٠'}, {'1', '١'}, {'2', '٢'}, {'3', '٣'}, {'4', '٤'},
        {'5', '٥'}, {'6', '٦'}, {'7', '٧'}, {'8', '٨'}, {'9', '٩'}
    };

    public static string FormatYear(int year, string language)
    {
        var yearString = year.ToString();

        if (string.Equals(language, "ar", StringComparison.OrdinalIgnoreCase))
        {
            return ConvertToArabicNumerals(yearString);
        }

        return yearString;
    }

    public static string FormatDate(DateTime date, string language)
    {
        var formattedDate = date.ToString("d/M/yyyy", EnglishCulture);

        if (string.Equals(language, "ar", StringComparison.OrdinalIgnoreCase))
        {
            return ConvertToArabicNumerals(formattedDate);
        }

        return formattedDate;
    }

    public static string FormatMonth(DateTime date, string language)
    {
        if (string.Equals(language, "ar", StringComparison.OrdinalIgnoreCase))
        {
            return ArabicCulture.DateTimeFormat.GetMonthName(date.Month);
        }

        return date.ToString("MMMM", EnglishCulture);
    }

    public static string FormatDay(DateTime date, string language)
    {
        var day = date.Day;

        if (string.Equals(language, "ar", StringComparison.OrdinalIgnoreCase))
        {
            return ConvertToArabicNumerals(day.ToString());
        }

        var suffix = GetOrdinalSuffix(day);
        return $"{day}{suffix}";
    }

    private static string ConvertToArabicNumerals(string input)
    {
        var result = new char[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            result[i] = ArabicNumerals.TryGetValue(input[i], out var arabicChar)
                ? arabicChar
                : input[i];
        }
        return new string(result);
    }

    private static string GetOrdinalSuffix(int day)
    {
        if (day >= 11 && day <= 13)
            return "th";

        return (day % 10) switch
        {
            1 => "st",
            2 => "nd",
            3 => "rd",
            _ => "th"
        };
    }

};


