using System.Windows;
using System.Windows.Controls;

namespace ES.Tools.TestApp.View
{
  public class CustomItemsControl : ItemsControl
  {
    protected override DependencyObject GetContainerForItemOverride()
    {
      return new ContentControl();
    }

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
      // Even wrap other ContentControls
      return false;
    }
  }
}
