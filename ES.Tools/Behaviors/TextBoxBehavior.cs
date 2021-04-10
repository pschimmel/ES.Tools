using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ES.Tools.Behaviors
{
  /// <summary>
  /// Behaviors to extend WPF TextBoxes.
  /// </summary>
  public static class TextBoxBehavior
  {
    /// <summary>
    /// Select all text when the <see cref="TextBox"/> is focused.
    /// </summary>
    public static readonly DependencyProperty SelectAllTextOnFocusProperty = DependencyProperty.RegisterAttached("SelectAllTextOnFocus", typeof(bool), typeof(TextBoxBehavior), new UIPropertyMetadata(false, OnSelectAllTextOnFocusChanged));

    /// <summary>
    /// Getter of the <see cref="SelectAllTextOnFocusProperty"/>.
    /// </summary>
    public static bool GetSelectAllTextOnFocus(TextBox textBox)
    {
      return (bool)textBox.GetValue(SelectAllTextOnFocusProperty);
    }

    /// <summary>
    /// Setter of the <see cref="SelectAllTextOnFocusProperty"/>.
    /// </summary>
    public static void SetSelectAllTextOnFocus(TextBox textBox, bool value)
    {
      textBox.SetValue(SelectAllTextOnFocusProperty, value);
    }

    private static void OnSelectAllTextOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is TextBox textBox && e.NewValue is bool value)
      {
        if (value)
        {
          textBox.GotFocus += TextBox_GotFocus;
          textBox.PreviewMouseDown += TextBox_PreviewMouseDown;
        }
        else
        {
          textBox.GotFocus -= TextBox_GotFocus;
          textBox.PreviewMouseDown -= TextBox_PreviewMouseDown;
        }
      }
    }

    private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
      if (e.OriginalSource is TextBox textBox)
      {
        textBox.SelectAll();
      }
    }

    private static void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
      if (sender is TextBox textBox && !textBox.IsKeyboardFocusWithin)
      {
        textBox.Focus();
        e.Handled = true;
      }
    }
  }
}
