using System.Globalization;
using ES.Tools.Converters;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Converters
{
  /// <summary>
  /// Unit tests testing the <see cref="IsNotNullConverter"/>.
  /// </summary>
  public class IsNotNullConverterTests
  {
    [Test]
    public void IsNotNullConverterTestConvert()
    {
      var converter = new IsNotNullConverter();
      Assert.That((bool)converter.Convert(null, typeof(Dummy), null, CultureInfo.InvariantCulture), Is.False);
      Assert.That((bool)converter.Convert(new Dummy(), typeof(Dummy), null, CultureInfo.InvariantCulture), Is.True);
    }

    public class Dummy
    {
    }
  }
}