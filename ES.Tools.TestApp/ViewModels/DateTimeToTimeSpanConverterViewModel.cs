using System;

namespace ES.Tools.TestApp.ViewModels
{
  public class DateTimeToTimeSpanConverterViewModel : ConverterViewModel
  {
    private TimeSpan _time;

    public DateTimeToTimeSpanConverterViewModel()
      : base("DateTimeToTimeSpanConverter")
    { }

    public TimeSpan Time
    {
      get => _time;
      set
      {
        if (_time != value)
        {
          _time = value;
          OnPropertyChanged();
        }
      }
    }
  }
}
