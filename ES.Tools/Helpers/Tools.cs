using System.ComponentModel;
using System.Windows;

namespace ES.Tools.Base
{
  public static class Tools
  {
    public static bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());
  }
}
