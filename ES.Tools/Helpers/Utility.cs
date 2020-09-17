using System.ComponentModel;
using System.Windows;

namespace ES.Tools.Helpers
{
  public static class Utility
  {
    public static bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());
  }
}
