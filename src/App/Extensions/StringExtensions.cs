namespace App.Extensions;

public static class StringExtensions
{
    public static bool IgnoreEquals(this string left, string right)
    {
        return string.Equals(left, right, StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsValidIpV4(this string input)
    {
        return !string.IsNullOrWhiteSpace(input) && input.Length <= 15;
    }
}