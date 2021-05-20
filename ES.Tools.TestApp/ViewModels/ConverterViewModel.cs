using ES.Tools.Core.MVVM;

namespace ES.Tools.TestApp.ViewModels
{
  public abstract class ConverterViewModel : ViewModel
  {
    protected ConverterViewModel(string description)
    {
      Description = description;
    }

    public string Description { get; }
  }
}
