using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace ES.Tools.Behaviors
{
  /// <summary>
  /// If an item gets selected, it will automatically scroll into view.
  /// </summary>
  public static class AutoScrollToCurrentItemBehavior
  {
    public static readonly DependencyProperty AutoScrollProperty = DependencyProperty.RegisterAttached("AutoScroll", typeof(bool), typeof(AutoScrollToCurrentItemBehavior), new UIPropertyMetadata(false, AutoScrollPropertyChanged));

    public static bool GetAutoScroll(Selector selector)
    {
      return (bool)selector.GetValue(AutoScrollProperty);
    }

    public static void SetAutoScroll(Selector selector, bool autoScroll)
    {
      selector.SetValue(AutoScrollProperty, autoScroll);
    }

    private static void AutoScrollPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is Selector selector)
      {
        selector.SelectionChanged += (sender, args) =>
        {
          // Sender and selector need to be the same. Otherwise sub-items (e.g. drop-down lists of comboboxes) will also trigger this.
          if (sender != selector || sender != args.OriginalSource)
          {
            return;
          }

          try
          {
            if (selector.SelectedItem != null)
            {
              selector.Dispatcher.BeginInvoke(new Action(() =>
              {
                if (sender is DataGrid dataGrid)
                {
                  dataGrid.ScrollIntoView(dataGrid.SelectedItem);
                }
                else if (sender is ListBox listBox)
                {
                  listBox.ScrollIntoView(listBox.SelectedItem);
                }
                else if (sender is ListView listView)
                {
                  listView.ScrollIntoView(listView.SelectedItem);
                }
                else
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
        };
      }
    }
  }
}
