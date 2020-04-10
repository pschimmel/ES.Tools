using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media;
using ES.WPF.Converters.ES.WPF.Converters;
using NUnit.Framework;

namespace ES.Tools.UnitTests
{
  public class ColorToStringConverterTests
  {
    // Don't use black, it is an indication that the test failed.
    private static readonly List<string> validValues = new List<string> { "Yellow", "#aacc44", "#ffaacc44", "#aaaacc44" };

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
        object result = converter.Convert(validValue, typeof(Color), null, CultureInfo.InvariantCulture);
        Assert.IsNotNull(result);
        Assert.AreNotEqual(Colors.Black, result);
      }
    }
  }
}