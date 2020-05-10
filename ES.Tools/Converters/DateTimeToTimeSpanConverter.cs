using System;
using System.Globalization;
using System.Windows.Data;

namespace ES.Tools.Converters
{
  /// <summary>
  /// Converts a DateTime to a TimeSpan and back. This can be used for Properties of type DateTime which actualy
  /// represent just a time component.
  /// </summary>
  [ValueConversion(typeof(TimeSpan?), typeof(DateTime?))]
  public class DateTimeToTimeSpanConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null)
      {
        return null;
      }

      if (!(value is TimeSpan))
      {
        throw new ArgumentException("Value must be a TimeSpan.");
      }

      var time = (TimeSpan)value;
      return DateTime.Today.Add(time);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null)
      {
        return null;
      }

      if (!(value is DateTime))
      {
        throw new ArgumentException("Value must be a DateTime.");
      }

      var date = (DateTime)value;
      return new TimeSpan(date.Hour, date.Minute, date.Second);
    }
  }
}