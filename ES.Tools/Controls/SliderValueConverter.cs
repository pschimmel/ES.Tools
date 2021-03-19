using System;
using System.Globalization;
using System.Windows.Data;

namespace ES.Tools.Controls
{
  [ValueConversion(typeof(string), typeof(double))]
  internal class SliderValueConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values == null || values.Length < 1)
      {
        return null;
      }

      double doubleValue = (double)values[0];

      return values.Length < 2 || string.IsNullOrWhiteSpace(values[1]?.ToString())
        ? doubleValue.ToString(culture)
        : doubleValue.ToString(values[1].ToString(), culture);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}