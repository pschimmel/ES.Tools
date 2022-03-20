using System.Globalization;
using System.Windows;
using ES.Tools.Converters;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Converters
{
  /// <summary>
  /// Unit tests testing the <see cref="BooleanToVisibilityConverter"/>.
  /// </summary>
  public class InverseBooleanToVisibilityConverterTests
  {
    /// <summary>
    /// Test conversion from boolean value to visibility.
    /// </summary>
    [Test]
    public void InverseBooleanToVisibilityConverterTestConvert()
    {
      var converter = new InverseBooleanToVisibilityConverter();

      Assert.That((Visibility)converter.Convert(false, typeof(Visibility), null, CultureInfo.InvariantCulture), Is.EqualTo(Visibility.Visible));
      Assert.That((Visibility)converter.Convert(true, typeof(Visibility), null, CultureInfo.InvariantCulture), Is.EqualTo(Visibility.Collapsed));
      Assert.That((Visibility)converter.Convert(null, typeof(Visibility), null, CultureInfo.InvariantCulture), Is.EqualTo(Visibility.Collapsed));

      Assert.That((Visibility)converter.Convert(false, typeof(Visibility), Visibility.Hidden, CultureInfo.InvariantCulture), Is.EqualTo(Visibility.Visible));
      Assert.That((Visibility)converter.Convert(true, typeof(Visibility), Visibility.Hidden, CultureInfo.InvariantCulture), Is.EqualTo(Visibility.Hidden));
      Assert.That((Visibility)converter.Convert(null, typeof(Visibility), Visibility.Hidden, CultureInfo.InvariantCulture), Is.EqualTo(Visibility.Hidden));
    }

    /// <summary>
    /// Test conversion from visibility to boolean value.
    /// </summary>
    [Test]
    public void InverseBooleanToVisibilityConverterTestConvertBack()
    {
      var converter = new InverseBooleanToVisibilityConverter();

      Assert.That((bool)converter.ConvertBack(Visibility.Visible, typeof(bool), null, CultureInfo.InvariantCulture), Is.False);
      Assert.That((bool)converter.ConvertBack(Visibility.Collapsed, typeof(bool), null, CultureInfo.InvariantCulture), Is.True);
      Assert.That((bool)converter.ConvertBack(Visibility.Hidden, typeof(bool), null, CultureInfo.InvariantCulture), Is.True);
    }
  }
}