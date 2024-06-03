using System.Globalization;

namespace Egress.Domain.Utils;

public static class StringExtensions
{
    #region Constants
    private const string WHITESPACE = " ";
    private const string UNDERLINE = "_";
    #endregion
    
    public static string NormalizeStringCustom(this string str)
        => new string(str
            .Normalize(System.Text.NormalizationForm.FormD)
            .Where(c => !CharUnicodeInfo.GetUnicodeCategory(c).Equals(UnicodeCategory.NonSpacingMark))
            .ToArray()).ToUpper().Replace(WHITESPACE, UNDERLINE);
}