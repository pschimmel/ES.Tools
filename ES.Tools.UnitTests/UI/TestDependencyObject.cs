using System.Windows;

namespace ES.Tools.UnitTests.UI
{
  internal class TestDependencyObject : DependencyObject
  {
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(TestDependencyObject), new PropertyMetadata(string.Empty));

    public string Text
    {
      get => (string)GetValue(TextProperty);
      set => SetValue(TextProperty, value);
    }
  }
}