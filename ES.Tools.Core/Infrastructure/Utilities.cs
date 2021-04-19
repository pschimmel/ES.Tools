using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;

namespace ES.Tools.Infrastructure
{
  /// <summary>
  /// General utilities class.
  /// </summary>
  public static class Utilities
  {
    /// <summary>
    /// Returns true if the application is currently running in design mode (Visual Studio Designer).
    /// </summary>
    public static bool IsDesignTime => DesignerProperties.GetIsInDesignMode(new DependencyObject());

    public static T Clone<T>(T uiElement) where T : UIElement
    {
      string xaml = GetUIElementAsString(uiElement);
      return GetUIElementFromString<T>(xaml);
    }

    public static string GetUIElementAsString<T>(T uiElement) where T : UIElement
    {
      return XamlWriter.Save(uiElement);
    }

    public static T GetUIElementFromString<T>(string xaml) where T : UIElement
    {
      return (T)(XamlReader.Parse(xaml) as UIElement);
    }
  }
}
