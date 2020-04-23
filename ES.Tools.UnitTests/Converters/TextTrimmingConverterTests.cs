using System.Collections.Generic;
using System.Globalization;
using ES.Tools.Converters;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Converters
{
  /// <summary>
  /// Unit tests testing the <see cref="TextTrimmingConverter"/>
  /// </summary>
  public class TextTrimmingConverterTests
  {
    private static readonly Dictionary<(string Input, int Number), string> values = new Dictionary<(string, int), string>
    {
      { ("Test", 100), "Test"},
      { ("Long test text", 5), "Long..."},
      { ("A bit longer test text", 5), "A bit..."}
    };

    [Test]
    public void ColorToStringConverterTestValidValues()
    {
      var converter = new TextTrimmingConverter();
      foreach (var value in values)
      {
        string result = (string)converter.Convert(value.Key.Input, typeof(string), value.Key.Number, CultureInfo.InvariantCulture);
        Assert.IsNotNull(result);
        Assert.AreEqual(value.Value, result);
      }
    }
  }
}