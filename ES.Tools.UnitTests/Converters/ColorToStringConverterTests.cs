using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media;
using NUnit.Framework;

namespace ES.Tools.Converters.UnitTests
{
  public class ColorToStringConverterTests
  {
    // Don't use black, it is an indication that the test failed.
    private static readonly List<string> validValues = new List<string> { "Yellow", "#aacc44", "#ffaacc44", "#aaaacc44" };
    private static readonly List<string> invalidValues = new List<string> { "Blah", "#aagg44", "#44" };

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ColorToStringConverterTestValidValues()
    {
      var converter = new ColorToStringConverter();
      foreach (string validValue in validValues)
      {
        var result = (Color)converter.Convert(validValue, typeof(Color), null, CultureInfo.InvariantCulture);
        Assert.IsNotNull(result);
        Assert.AreNotEqual(Colors.Black, result);
      }
    }

    [Test]
    public void ColorToStringConverterTestInvalidValues()
    {
      var converter = new ColorToStringConverter();

      foreach (string validValue in invalidValues)
      {
        Assert.Throws<ApplicationException>(() => converter.Convert(validValue, typeof(Color), null, CultureInfo.InvariantCulture));
      }
    }
  }
}