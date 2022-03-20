using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ES.Tools.Converters
{
  /// <summary>
  /// Converts the availability of an object into a boolean value. Only possible for one way bindings.
  /// </summary>
  [ValueConversion(typeof(object), typeof(bool))]
  public class IsNullConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value == null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return DependencyProperty.UnsetValue;
    }
  }
}
