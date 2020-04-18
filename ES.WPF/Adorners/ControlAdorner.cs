using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using ES.WPF.Helpers;

namespace ES.WPF.Adorners
{
  /// <summary>
  /// Adorner that renders a control.
  /// </summary>
  public class ControlAdorner : Adorner, IDisposable
  {
    private FrameworkElement _child;
    private readonly AdornerLayer _adornerLayer;

    public ControlAdorner(UIElement adornedElement, AdornerLayer adornerLayer = null)
      : base(adornedElement)
    {
      _adornerLayer = adornerLayer ?? AdornerLayer.GetAdornerLayer(adornedElement);
    }

    protected override int VisualChildrenCount => 1;

    protected override Visual GetVisualChild(int index)
    {
      if (index != 0)
      {
        throw new ArgumentOutOfRangeException();
      }

      return _child;
    }

    public FrameworkElement Child
    {
      get => _child;
      set
      {
        if (_child != null)
        {
          RemoveVisualChild(_child);
        }
        _child = value;
        if (_child != null)
        {
          var parent = _child.GetVisualParent<ControlAdorner>();
          if (parent != null)
          {
            parent.Child = null;
          }

          AddVisualChild(_child);
        }
      }
    }

    protected override Size MeasureOverride(Size constraint)
    {
      _child.Measure(constraint);
      return _child.DesiredSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      var window = AdornedElement.GetParent<Window>();
      var relativePoint = AdornedElement.TransformToAncestor(window).Transform(new Point(0, 0));
      var adornerPoint = new Point(0, AdornedElement.RenderSize.Height);
      if (relativePoint.Y + finalSize.Height + 40 > window.Height)
      {
        adornerPoint = new Point(0, finalSize.Height * -1);
      }

      _child.Arrange(new Rect(adornerPoint, finalSize));
      return new Size(_child.ActualWidth, _child.ActualHeight);
    }

    /// <summary>
    /// Dispose() calls Dispose(true)
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Cleanup.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        _adornerLayer.Remove(this);
      }
    }
  }
}
