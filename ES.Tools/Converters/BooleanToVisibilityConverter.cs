using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ES.Tools.Converters
{
  /// <summary>
  /// Converts a boolean value into <see cref="Visibility>."/>
  /// </summary>
  [ValueConversion(typeof(bool), typeof(Visibility))]
  public class BooleanToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (parameter is not Visibility falseValue)
      {
        falseValue = Visibility.Collapsed;
      }

      if (value == null)
      {
        return falseValue;
      }

      bool b = (bool)value;
      return b ? Visibility.Visible : falseValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var vValue = (Visibility)value;
      return !(vValue == Visibility.Collapsed || vValue == Visibility.Hidden);
    }
  }
}
