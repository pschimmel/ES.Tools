using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ES.Tools.UI
{
  /// <summary>
  /// WPF Extension methods that helps navigating through visual tree and logical tree.
  /// </summary>
  public static class TreeHelper
  {
    #region Public Methods

    /// <summary>
    /// Get the parent object of a certain type by using the logical or visual tree.
    /// </summary>
    public static T GetParent<T>(this DependencyObject obj, Func<T, bool> predicate = null) where T : DependencyObject
    {
      return GetParentInternal(obj, x => LogicalTreeHelper.GetParent(x) ?? GetParentObject(x), predicate);
    }

    /// <summary>
    /// Get the parent object of a certain type by using the logical tree.
    /// </summary>
    public static T GetLogicalParent<T>(this DependencyObject obj, Func<T, bool> predicate = null) where T : DependencyObject
    {
      return GetParentInternal(obj, x => LogicalTreeHelper.GetParent(x), predicate);
    }

    /// <summary>
    /// Get the parent object of a certain type by using the visual tree.
    /// </summary>
    public static T GetVisualParent<T>(this DependencyObject obj, Func<T, bool> predicate = null) where T : DependencyObject
    {
      return GetParentInternal(obj, x => GetParentObject(x), predicate);
    }

    /// <summary>
    /// Returns true, if the current object is in the visual tree of the parent object.
    /// </summary>
    public static bool IsChildOf(this DependencyObject child, DependencyObject parent)
    {
      var parentObject = child.GetParentObject();
      return parentObject != null && (parentObject == parent || parentObject.IsChildOf(parent));
    }

    /// <summary>
    /// Returns the child elements of a certain type using the visual tree.
    /// </summary>
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

    /// <summary>
    /// Returns the child elements of a certain type using the logical tree.
    /// </summary>
    public static T GetLogicalChild<T>(this DependencyObject obj) where T : DependencyObject
    {
      if (obj == null)
      {
        return null;
      }

      foreach (var child in LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>())
      {
        var result = (child as T) ?? GetVisualChild<T>(child);
        if (result != null)
        {
          return result;
        }
      }
      return null;
    }

    /// <summary>
    /// Returns a list of child elements of a certain type using the visual tree.
    /// </summary>
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
    /// Returns a list of child elements of a certain type using the logical tree.
    /// </summary>
    public static IEnumerable<T> GetLogicalChildren<T>(this DependencyObject obj) where T : DependencyObject
    {
      if (obj == null)
      {
        return null;
      }

      var list = new List<T>();
      foreach (var child in LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>())
      {
        if (child is T)
        {
          list.Add(child as T);
        }
        else
        {
          GetLogicalChildren<T>(child)?.ToList().ForEach(x => list.Add(x));
        }
      }
      return list;
    }

    /// <summary>
    /// Returns the parent window of a <see cref="DependencyObject"/>.
    /// </summary>
    public static Window GetWindow(this DependencyObject obj)
    {
      return Window.GetWindow(obj);
    }

    #endregion

    #region Private Methods

    private static T GetParentInternal<T>(DependencyObject obj, Func<DependencyObject, DependencyObject> getParent, Func<T, bool> predicate = null) where T : DependencyObject
    {
      var result = getParent(obj);
      while (result != null && !(result is T && (predicate?.Invoke(result as T) ?? true)))
      {
        result = getParent(result);
      }
      return result as T;
    }

    /// <summary>
    /// Returns the parent object of a WPF control, but also supports content elements.
    /// </summary>
    /// <returns>The item's parent, if available. Otherwise null.</returns>
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

    #endregion
  }
}