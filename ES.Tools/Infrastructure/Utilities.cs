using System.ComponentModel;
using System.Windows;

namespace ES.Tools.Infrastructure
{
  public static class Utilities
  {
    public static bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());
  }
}
