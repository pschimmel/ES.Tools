namespace ES.Tools.Core.Text
{
  public static class TextExtensions
  {
    /// <summary>
    /// Strips the BOM (Byte order mark) U+FEFF and zero width space U+200B from a string.
    /// </summary>
    /// <returns>
    /// Clean string without byte order mark.
    /// </returns>
    public static string StripBOM(this string s)
    {
      return s?.Trim(new char[] { '\uFEFF', '\u200B' }) ?? null;
    }
  }
}
