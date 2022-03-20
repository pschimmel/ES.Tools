using System.Globalization;
using ES.Tools.Converters;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Converters
{
  /// <summary>
  /// Unit tests testing the <see cref="NegationConverter"/>.
  /// </summary>
  public class NegationConverterTests
  {
    [Test]
    public void NegationConverterTestConvert()
    {
      var converter = new NegationConverter();
      Assert.That((bool)converter.Convert(false, typeof(bool), null, CultureInfo.InvariantCulture), Is.True);
      Assert.That((bool)converter.Convert(true, typeof(bool), null, CultureInfo.InvariantCulture), Is.False);
    }
  }
}