using System.Globalization;
using ES.Tools.Converters;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Converters
{
  /// <summary>
  /// Unit tests testing the <see cref="IsNullConverter"/>.
  /// </summary>
  public class IsNullConverterTests
  {
    [Test]
    public void IsNullConverterTestConvertValue()
    {
      var converter = new IsNullConverter();
      Assert.That((bool)converter.Convert(null, typeof(Dummy), null, CultureInfo.InvariantCulture), Is.True);
      Assert.That((bool)converter.Convert(new Dummy(), typeof(Dummy), null, CultureInfo.InvariantCulture), Is.False);
    }

    public class Dummy
    {
    }
  }
}