using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ES.Tools.Behaviors
{
  /// <summary>
  /// Behaviors to extend WPF PasswordBoxes.
  /// </summary>
  public static class PasswordBoxBehavior
  {
    /// <summary>
    /// Select all text when the <see cref="PasswordBox"/> is focused.
    /// </summary>
    public static readonly DependencyProperty SelectAllTextOnFocusProperty = DependencyProperty.RegisterAttached("SelectAllTextOnFocus", typeof(bool), typeof(PasswordBoxBehavior), new UIPropertyMetadata(false, OnSelectAllTextOnFocusChanged));

    /// <summary>
    /// Getter of the <see cref="SelectAllTextOnFocusProperty"/>.
    /// </summary>
    public static bool GetSelectAllTextOnFocus(PasswordBox passwordBox)
    {
      return (bool)passwordBox.GetValue(SelectAllTextOnFocusProperty);
    }

    /// <summary>
    /// Setter of the <see cref="SelectAllTextOnFocusProperty"/>.
    /// </summary>
    public static void SetSelectAllTextOnFocus(PasswordBox passwordBox, bool value)
    {
      passwordBox.SetValue(SelectAllTextOnFocusProperty, value);
    }

    private static void OnSelectAllTextOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is PasswordBox passwordBox)
      {
        if (e.NewValue is bool == false)
        {
          return;
        }

        if ((bool)e.NewValue)
        {
          passwordBox.GotFocus += PasswordBox_GotFocus;
          passwordBox.PreviewMouseDown += PasswordBox_PreviewMouseDown;
        }
        else
        {
          passwordBox.GotFocus -= PasswordBox_GotFocus;
          passwordBox.PreviewMouseDown -= PasswordBox_PreviewMouseDown;
        }
      }
    }

    private static void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
    {
      if (e.OriginalSource is PasswordBox passwordBox)
      {
        passwordBox.SelectAll();
      }
    }

    private static void PasswordBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
      if (sender is PasswordBox passwordBox && !passwordBox.IsKeyboardFocusWithin)
      {
        passwordBox.Focus();
        e.Handled = true;
      }
    }
  }
}
