using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ES.WPF.Converters
{
  namespace ES.WPF.Converters
  {
    /// <summary>
    /// Converts a WPF color to a string. This can be used to store colors to databases or settings files.
    /// </summary>
    [ValueConversion(typeof(Color), typeof(string))]
    public class ColorToStringConverter : IValueConverter
    {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        if (value == null)
        {
          return Colors.Black;
        }

        string strValue = value.ToString();
        try
        {
          return (Color)ColorConverter.ConvertFromString(strValue);
        }
        catch (Exception ex)
        {
          Debug.Fail(ex.Message);
        }

        return Colors.Black;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        if (value == null)
        {
          return null;
        }

        var color = (Color)value;
        return color.ToString();
      }
    }
  }
}
