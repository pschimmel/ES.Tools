using ES.Tools.MVVM;

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

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      DialogResult = true;
    }
  }
}
