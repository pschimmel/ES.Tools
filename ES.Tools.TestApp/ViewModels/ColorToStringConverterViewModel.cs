namespace ES.Tools.TestApp.ViewModels
{
  public class ColorToStringConverterViewModel : ConverterViewModel
  {
    private string _colorText;

    public ColorToStringConverterViewModel()
      : base("ColorToStringConverter")
    {
      ColorText = "Yellow";
    }

    public string ColorText
    {
      get => _colorText;
      set
      {
        if (_colorText != value)
        {
          _colorText = value;
          OnPropertyChanged();
        }
      }
    }
  }
}
