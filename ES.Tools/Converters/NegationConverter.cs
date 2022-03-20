using System;
using System.Globalization;
using System.Windows.Data;

namespace ES.Tools.Converters
{
  /// <summary>
  /// Negates a boolean value.
  /// </summary>
  [ValueConversion(typeof(bool), typeof(bool))]
  public class NegationConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (bool)value == false ? true : (object)false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (bool)value == false ? true : (object)false;
    }
  }
}
