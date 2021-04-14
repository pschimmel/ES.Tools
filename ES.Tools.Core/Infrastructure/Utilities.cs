using System.ComponentModel;
using System.Windows;

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
    public static bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());
  }
}
