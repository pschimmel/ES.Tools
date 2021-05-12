using ES.Tools.Core.MVVM;

namespace ES.Tools.TestApp.Views
{
  /// <summary>
  /// Interaction logic for SampleWindow.xaml
  /// </summary>
  public partial class SampleWindow : IView
  {
    public SampleWindow()
    {
      InitializeComponent();
    }

    public IViewModel ViewModel
    {
      get => DataContext as IViewModel;
      set => DataContext = value;
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      DialogResult = true;
    }
  }
}
