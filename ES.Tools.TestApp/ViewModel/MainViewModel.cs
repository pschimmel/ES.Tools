using System.Collections.Generic;

namespace ES.Tools.TestApp.ViewModel
{
  public class MainViewModel
  {
    public MainViewModel()
    {
      Converters = new List<ConverterViewModel>
      {
        new ColorToStringConverterViewModel()
      };
    }

    public List<ConverterViewModel> Converters { get; }
  }
}
