using ES.Tools.MVVM;

namespace ES.Tools.TestApp.ViewModels
{
  public class AccordionContentViewModel : ViewModel
  {
    public AccordionContentViewModel(string header, object content)
    {
      Header = header;
      Content = content;
    }

    public string Header { get; }

    public object Content { get; }
  }
}