using System.Windows;
using System.Windows.Controls;

namespace ES.Tools.Controls
{
  /// <summary>
  /// Items control that uses a ContentControl as item container. This enables implicit VM data templates.
  /// </summary>
  public class CustomItemsControl : ItemsControl
  {
    protected override DependencyObject GetContainerForItemOverride()
    {
      return new ContentControl();
    }

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
      // Wrap other ContentControls
      return false;
    }
  }
}
