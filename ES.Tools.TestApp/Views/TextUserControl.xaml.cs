using System.Windows;
using System.Windows.Controls;

namespace ES.Tools.TestApp.Views
{
  /// <summary>
  /// Interaction logic for TextUserControl.xaml
  /// </summary>
  public partial class TextUserControl : UserControl
  {
    public TextUserControl()
    {
      InitializeComponent();
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(TextUserControl), new PropertyMetadata(string.Empty));

    public string Text
    {
      get => (string)GetValue(TextProperty);
      set => SetValue(TextProperty, value);
    }
  }
}
