namespace ES.Tools.TestApp.ViewModels
{
  public class NegationConverterViewModel : ConverterViewModel
  {
    private bool _value;

    public NegationConverterViewModel()
      : base("NegationConverter")
    { }

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
