using System.Text.RegularExpressions;

namespace Catalog.Domain.Utils;

public static class SlugGenerator
{
    public static string GenerateSlug(this string phrase)
    {
        if (String.IsNullOrWhiteSpace(phrase))
        {
            return string.Empty;
        }
        
        string str = phrase.ToLower();
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        str = Regex.Replace(str, @"\s+", " ").Trim();
        str = str.Substring(0, str.Length <= 90 ? str.Length : 90).Trim();
        str = Regex.Replace(str, @"\s", "-");
        return str;
    }
}