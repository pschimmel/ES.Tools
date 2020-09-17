using System.Globalization;
using System.Windows;
using ES.Tools.Converters;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Converters
{
  /// <summary>
  /// Unit tests testing the <see cref="BooleanToVisibilityConverter"/>.
  /// </summary>
  public class BooleanToVisibilityConverterTests
  {
    /// <summary>
    /// Test conversion from boolean value to visibility.
    /// </summary>
    [Test]
    public void BooleanToVisibilityConverterTestValidValues()
    {
      var converter = new BooleanToVisibilityConverter();

      Assert.AreEqual(Visibility.Visible, (Visibility)converter.Convert(true, typeof(Visibility), null, CultureInfo.InvariantCulture));
      Assert.AreEqual(Visibility.Collapsed, (Visibility)converter.Convert(false, typeof(Visibility), null, CultureInfo.InvariantCulture));
      Assert.AreEqual(Visibility.Collapsed, (Visibility)converter.Convert(null, typeof(Visibility), null, CultureInfo.InvariantCulture));

      Assert.AreEqual(Visibility.Visible, (Visibility)converter.Convert(true, typeof(Visibility), Visibility.Hidden, CultureInfo.InvariantCulture));
      Assert.AreEqual(Visibility.Hidden, (Visibility)converter.Convert(false, typeof(Visibility), Visibility.Hidden, CultureInfo.InvariantCulture));
      Assert.AreEqual(Visibility.Hidden, (Visibility)converter.Convert(null, typeof(Visibility), Visibility.Hidden, CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Test conversion from visibility to boolean value.
    /// </summary>
    [Test]
    public void BooleanToVisibilityConverterTestInvalidValues()
    {
      var converter = new BooleanToVisibilityConverter();

      Assert.AreEqual(true, (bool)converter.ConvertBack(Visibility.Visible, typeof(bool), null, CultureInfo.InvariantCulture));
      Assert.AreEqual(false, (bool)converter.ConvertBack(Visibility.Collapsed, typeof(bool), null, CultureInfo.InvariantCulture));
      Assert.AreEqual(false, (bool)converter.ConvertBack(Visibility.Hidden, typeof(bool), null, CultureInfo.InvariantCulture));
    }
  }
}