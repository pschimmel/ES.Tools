using System.Collections.Generic;
using System.Globalization;
using ES.Tools.Converters;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Converters
{
  /// <summary>
  /// Unit tests testing the <see cref="TextTrimmingConverter"/>.
  /// </summary>
  public class TextTrimmingConverterTests
  {
    private static readonly Dictionary<(string Input, int Length), string> values = new()
    {
      { ("Test", 100), "Test"},
      { ("Long test text", 5), "Long..."},
      { ("A bit longer test text", 5), "A bit..."},
      { (null, 100), null}
    };

    [Test]
    public void ColorToStringConverterTestConvert()
    {
      var converter = new TextTrimmingConverter();

      foreach (var value in values)
      {
        string result = (string)converter.Convert(value.Key.Input, typeof(string), value.Key.Length, CultureInfo.InvariantCulture);

        if (value.Key.Input == null)
        {
          Assert.That(result, Is.Null);
        }
        else
        {
          Assert.That(result, Is.Not.Null);
        }

        Assert.That(result, Is.EqualTo(value.Value));
      }
    }
  }
}