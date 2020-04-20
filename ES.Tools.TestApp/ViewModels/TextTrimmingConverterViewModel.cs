namespace ES.Tools.TestApp.ViewModels
{
  public class TextTrimmingConverterViewModel : ConverterViewModel
  {
    private string _text;

    public TextTrimmingConverterViewModel()
      : base("TextTrimmingConverter")
    { }

    public string Text
    {
      get => _text;
      set
      {
        if (_text != value)
        {
          _text = value;
          OnPropertyChanged();
        }
      }
    }
  }
}
