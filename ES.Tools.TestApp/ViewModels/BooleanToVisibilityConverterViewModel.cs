namespace ES.Tools.TestApp.ViewModels
{
  public class BooleanToVisibilityConverterViewModel : ConverterViewModel
  {
    private bool _value;

    public BooleanToVisibilityConverterViewModel()
      : base("BooleanToVisibilityConverter")
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
