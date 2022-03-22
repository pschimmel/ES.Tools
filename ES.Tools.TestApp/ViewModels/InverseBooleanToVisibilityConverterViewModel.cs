namespace ES.Tools.TestApp.ViewModels
{
  public class InverseBooleanToVisibilityConverterViewModel : ConverterViewModel
  {
    private bool _value;

    public InverseBooleanToVisibilityConverterViewModel()
      : base("InverseBooleanToVisibilityConverter")
    {
    }

    public bool Value
    {
      get => _value;
      set
      {
        if (_value != value)
        {
          _value = value;
          OnPropertyChanged();
        }
      }
    }
  }
}
