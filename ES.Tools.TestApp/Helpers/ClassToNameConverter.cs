using System;
using System.Globalization;
using System.Windows.Data;

namespace ES.Tools.TestApp.Helpers
{
  internal class ClassToTypeNameConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null)
      {
        return "(null)";
      }

      var type = value.GetType();
      return type.Name;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
