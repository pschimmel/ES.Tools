using System;
using System.Windows;
using System.Windows.Input;

namespace ES.Tools.Behaviors
{
  /// <summary>
  /// Behavior that manages drag and drop of files.
  /// </summary>
  public static class FileDragDropBehavior
  {
    #region IsFileDragDropEnabledProperty

    public static readonly DependencyProperty IsFileDragDropEnabledProperty = DependencyProperty.RegisterAttached("IsFileDragDropEnabled", typeof(bool), typeof(FileDragDropBehavior), new UIPropertyMetadata(false, OnFileDragDropEnabled));

    public static bool GetIsFileDragDropEnabled(UIElement obj)
    {
      return (bool)obj.GetValue(IsFileDragDropEnabledProperty);
    }

    public static void SetIsFileDragDropEnabled(UIElement obj, bool value)
    {
      obj.SetValue(IsFileDragDropEnabledProperty, value);
    }

    private static void OnFileDragDropEnabled(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (e.NewValue == e.OldValue)
      {
        return;
      }

      if (d is UIElement element)
      {
        if ((bool)e.NewValue)
        {
          element.PreviewGiveFeedback += Element_PreviewGiveFeedback;
          element.PreviewDragOver += Element_PreviewDragOver;
          element.Drop += Element_Drop;
        }
        else
        {
          element.PreviewGiveFeedback -= Element_PreviewGiveFeedback;
          element.PreviewDragOver -= Element_PreviewDragOver;
          element.Drop -= Element_Drop;
        }
      }
    }

    #endregion

    #region FileDragDropTargetProperty

    public static readonly DependencyProperty FileDragDropTargetProperty = DependencyProperty.RegisterAttached("FileDragDropTarget", typeof(object), typeof(FileDragDropBehavior), null);

    public static bool GetFileDragDropTarget(UIElement obj)
    {
      return (bool)obj.GetValue(FileDragDropTargetProperty);
    }

    public static void SetFileDragDropTarget(UIElement obj, bool value)
    {
      obj.SetValue(FileDragDropTargetProperty, value);
    }

    #endregion

    #region Private Members

    private static void Element_PreviewGiveFeedback(object sender, GiveFeedbackEventArgs e)
    {
      if (e.Effects.HasFlag(DragDropEffects.Copy))
      {
        Mouse.SetCursor(Cursors.Cross);
      }
      else
      {
        Mouse.SetCursor(Cursors.No);
      }
      e.Handled = true;
    }

    private static void Element_PreviewDragOver(object sender, DragEventArgs e)
    {
      if (sender is not DependencyObject d)
      {
        return;
      }

      if (d.GetValue(FileDragDropTargetProperty) is not IFileDragDropTarget fileTarget)
      {
        throw new Exception("FileDragDropTarget object must be of type IFileDragDropTarget");
      }

      e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop)
        && e.Data.GetData(DataFormats.FileDrop) is string[] pathList
        && fileTarget.AllowDrop(pathList)
          ? DragDropEffects.Copy
          : DragDropEffects.None;

      e.Handled = true;
    }

    private static void Element_Drop(object sender, DragEventArgs e)
    {
      if (sender is not DependencyObject d)
      {
        return;
      }

      if (d.GetValue(FileDragDropTargetProperty) is not IFileDragDropTarget fileTarget)
      {
        throw new Exception("FileDragDropTarget object must be of type IFileDragDropTarget");
      }

      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        fileTarget.OnFileDrop((string[])e.Data.GetData(DataFormats.FileDrop));
      }
    }

    #endregion
  }
}
