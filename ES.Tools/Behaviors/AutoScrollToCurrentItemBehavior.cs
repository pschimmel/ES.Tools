using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace ES.Tools.Behaviors
{
  /// <summary>
  /// Behavior that extends items that the currently selected element gets selected,
  /// </summary>
  public static class AutoScrollToCurrentItemBehavior
  {
    /// <summary>
    /// When an item of a <see cref="Selector"/> gets selected, it will automatically scroll into view.
    /// </summary>
    public static readonly DependencyProperty AutoScrollToCurrentItemProperty = DependencyProperty.RegisterAttached("AutoScrollToCurrentItem", typeof(bool), typeof(AutoScrollToCurrentItemBehavior), new UIPropertyMetadata(false, AutoScrollToCurrentItemPropertyChanged));

    public static bool GetAutoScrollToCurrentItem(Selector selector)
    {
      return (bool)selector.GetValue(AutoScrollToCurrentItemProperty);
    }

    public static void SetAutoScrollToCurrentItem(Selector selector, bool autoScroll)
    {
      selector.SetValue(AutoScrollToCurrentItemProperty, autoScroll);
    }

    private static void AutoScrollToCurrentItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is Selector selector && e.NewValue is bool)
      {
        if ((bool)e.NewValue)
        {
          selector.SelectionChanged += OnSelectionChanged;
        }
        else
        {
          selector.SelectionChanged -= OnSelectionChanged;
        }
      }
    }

    private static void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // Sender and selector need to be the same. Otherwise sub-items (e.g. drop-down lists of comboboxes) will also trigger this.
      if (!(sender is Selector selector) || sender != e.OriginalSource)
      {
        return;
      }

      try
      {
        if (selector.SelectedItem != null)
        {
          selector.Dispatcher.BeginInvoke(new Action(() =>
          {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem != null)
            {
              dataGrid.ScrollIntoView(dataGrid.SelectedItem);
            }
            else if (sender is ListBox listBox && listBox.SelectedItem != null)
            {
              listBox.ScrollIntoView(listBox.SelectedItem);
            }
            else if (sender is ListView listView && listView.SelectedItem != null)
            {
              listView.ScrollIntoView(listView.SelectedItem);
            }
            else if (selector.SelectedItem != null)
            {
              var dependencyObject = selector.ItemContainerGenerator.ContainerFromItem(selector.SelectedItem);
              if (dependencyObject is FrameworkElement frameworkElement)
              {
                frameworkElement.BringIntoView();
              }
            }
          }), DispatcherPriority.ContextIdle);
        }
      }
      catch (Exception ex)
      {
        Debug.Fail(ex.Message);
      }
    }

    public static readonly DependencyProperty AutoBringIntoViewProperty = DependencyProperty.RegisterAttached("AutoBringIntoView", typeof(bool), typeof(AutoScrollToCurrentItemBehavior), new UIPropertyMetadata(false, OnAutoBringIntoViewChanged));

    public static bool GetAutoBringIntoView(TreeViewItem treeViewItem)
    {
      return (bool)treeViewItem.GetValue(AutoBringIntoViewProperty);
    }

    public static void SetAutoBringIntoView(TreeViewItem treeViewItem, bool value)
    {
      treeViewItem.SetValue(AutoBringIntoViewProperty, value);
    }

    private static void OnAutoBringIntoViewChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is TreeViewItem item && e.NewValue is bool)
      {
        if ((bool)e.NewValue)
        {
          item.Selected += OnTreeViewItemSelected;
        }
        else
        {
          item.Selected -= OnTreeViewItemSelected;
        }
      }
    }

    private static void OnTreeViewItemSelected(object sender, RoutedEventArgs e)
    {
      // Only react to the Selected event raised by the TreeViewItem
      // whose IsSelected property was modified. Ignore all ancestors
      // who are merely reporting that a descendant's Selected fired.
      if (!ReferenceEquals(sender, e.OriginalSource))
      {
        return;
      }

      if (e.OriginalSource is TreeViewItem item)
      {
        item.BringIntoView();
      }
    }
  }
}
