using System.Collections.Generic;

namespace ES.Tools.TestApp.ViewModels
{
  public class MainViewModel : MVVM.ViewModel
  {
    public MainViewModel()
    {
      Converters = new List<ConverterViewModel>
      {
        new ColorToStringConverterViewModel(),
        new TextTrimmingConverterViewModel(),
        new DateTimeToTimeSpanConverterViewModel()
      };
    }

    public List<ConverterViewModel> Converters { get; }
  }
}
