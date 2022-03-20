using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ES.Tools.Converters
{
  /// <summary>
  /// Converts a boolean value into <see cref="Visibility"/>. This is the inverse variant. True means invisible.
  /// </summary>
  [ValueConversion(typeof(bool), typeof(Visibility))]
  public class InverseBooleanToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (parameter is not Visibility trueValue)
      {
        trueValue = Visibility.Collapsed;
      }

      if (value == null)
      {
        return trueValue;
      }

      bool b = (bool)value;
      return b ? trueValue : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var vValue = (Visibility)value;
      return vValue == Visibility.Collapsed || vValue == Visibility.Hidden;
    }
  }
}
