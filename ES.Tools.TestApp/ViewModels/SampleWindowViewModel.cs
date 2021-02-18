using ES.Tools.MVVM;

namespace ES.Tools.TestApp.ViewModels
{
  public class SampleWindowViewModel : ViewModel
  {
    public SampleWindowViewModel()
    {
      Message = "Sample Message";
    }

    public SampleWindowViewModel(string message)
    {
      Message = message;
    }

    public string Message { get; }
  }
}
