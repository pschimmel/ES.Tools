using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ES.WPF.Converters
{
  /// <summary>
  /// Converts a string to a string with a maximum length. Converter parameter is used to transfer the number of letters.
  /// </summary>
  [ValueConversion(typeof(string), typeof(string))]
  public class TextTrimmingConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null)
      {
        return null;
      }

      string s = value.ToString();
      int.TryParse(parameter.ToString(), out int length);
      return s.Length < length ? s : s.Substring(0, length).Trim() + "...";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return DependencyProperty.UnsetValue;
    }
  }
}
