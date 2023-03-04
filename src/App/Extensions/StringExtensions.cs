using System.Text.RegularExpressions;

namespace App.Extensions;

public static class StringExtensions
{
    private static readonly Regex IpV4Regex = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$", RegexOptions.Compiled);
    
    public static bool IgnoreEquals(this string left, string right)
    {
        return string.Equals(left, right, StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsValidIpV4(this string input)
    {
        return !string.IsNullOrWhiteSpace(input) && IpV4Regex.IsMatch(input);
    }
}