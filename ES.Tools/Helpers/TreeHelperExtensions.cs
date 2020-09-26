using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ES.Tools.Helpers
{
  /// <summary>
  /// WPF Extension methods that helps navigating through visual tree and logical tree.
  /// </summary>
  public static class TreeHelperExtensions
  {
    /// <summary>
    /// Get the parent object of a certain type by using the logical or visual tree.
    /// </summary>
    public static T GetParent<T>(this DependencyObject obj, Func<T, bool> check = null) where T : DependencyObject
    {
      return GetParentInternal(obj, x => LogicalTreeHelper.GetParent(x) ?? GetParentObject(x), check);
    }

    /// <summary>
    /// Get the parent object of a certain type by using the logical tree.
    /// </summary>
    public static T GetLogicalParent<T>(this DependencyObject obj, Func<T, bool> check = null) where T : DependencyObject
    {
      return GetParentInternal(obj, x => LogicalTreeHelper.GetParent(x), check);
    }

    /// <summary>
    /// Get the parent object of a certain type by using the visual tree.
    /// </summary>
    public static T GetVisualParent<T>(this DependencyObject obj, Func<T, bool> check = null) where T : DependencyObject
    {
      return GetParentInternal(obj, x => GetParentObject(x), check);
    }

    private static T GetParentInternal<T>(DependencyObject obj, Func<DependencyObject, DependencyObject> getParent, Func<T, bool> check = null) where T : DependencyObject
    {
      var result = getParent(obj);
      while (result != null && !(result is T && (check?.Invoke(result as T) ?? true)))
      {
        result = getParent(result);
      }
      return result as T;
    }

    public static T GetVisualChild<T>(this DependencyObject obj) where T : DependencyObject
    {
      if (obj == null)
      {
        return null;
      }

      for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
      {
        var child = VisualTreeHelper.GetChild(obj, i);
        var result = (child as T) ?? GetVisualChild<T>(child);
        if (result != null)
        {
          return result;
        }
      }
      return null;
    }

    public static IEnumerable<T> GetVisualChildren<T>(this DependencyObject obj) where T : DependencyObject
    {
      if (obj == null)
      {
        return null;
      }

      var list = new List<T>();
      for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
      {
        var child = VisualTreeHelper.GetChild(obj, i);
        if (child is T)
        {
          list.Add(child as T);
        }
        else
        {
          GetVisualChildren<T>(child)?.ToList().ForEach(x => list.Add(x));
        }
      }
      return list;
    }

    /// <summary>
    /// Returns the parent object of a WPF control, but also supports content elements.
    /// </summary>
    /// <returns>The submitted item's parent, if available. Otherwise null.</returns>
    private static DependencyObject GetParentObject(this DependencyObject child)
    {
      if (child == null)
      {
        return null;
      }

      // Special handling of handle content elements.
      if (child is ContentElement contentElement)
      {
        var parent = ContentOperations.GetParent(contentElement);
        if (parent != null)
        {
          return parent;
        }

        var fce = contentElement as FrameworkContentElement;
        return fce?.Parent;
      }

      // Also try searching for parent in framework elements (such as DockPanel, etc)
      if (child is FrameworkElement frameworkElement)
      {
        var parent = frameworkElement.Parent;
        if (parent != null)
        {
          return parent;
        }
      }

      // If it's not a ContentElement/FrameworkElement, fall back to VisualTreeHelper
      return VisualTreeHelper.GetParent(child);
    }
  }
}
