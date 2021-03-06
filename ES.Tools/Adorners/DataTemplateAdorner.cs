﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ES.Tools.Adorners
{
  /// <summary>
  /// Adorner that takes a data template and a ViewModel.
  /// </summary>
  public class DataTemplateAdorner : Adorner, IDisposable
  {
    private readonly ContentPresenter _contentPresenter;
    private readonly AdornerLayer _adornerLayer;
    private double _leftOffset;
    private double _topOffset;

    /// <summary>
    /// Constructor. Creates a new <see cref="DataTemplateAdorner"/> instance.
    /// </summary>
    /// <param name="adornedElement">The adorned element.</param>
    /// <param name="content">The content (usually the ViewModel).</param>
    /// <param name="dataTemplate">DataTemplate</param>
    /// <param name="adornerLayer">The adorner layer where the adorner will be rendered on (optionally)</param>
    public DataTemplateAdorner(UIElement adornedElement, object content, DataTemplate dataTemplate, AdornerLayer adornerLayer = null)
      : base(adornedElement)
    {
      _contentPresenter = new ContentPresenter { Content = content, ContentTemplate = dataTemplate };
      _adornerLayer = adornerLayer ?? AdornerLayer.GetAdornerLayer(adornedElement);

      // Automatically add to adorner layer of control
      if (adornerLayer == null)
      {
        _adornerLayer.Add(this);
      }
    }

    protected override Size MeasureOverride(Size constraint)
    {
      _contentPresenter.Measure(constraint);
      return _contentPresenter.DesiredSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      _contentPresenter.Arrange(new Rect(finalSize));
      return finalSize;
    }

    protected override Visual GetVisualChild(int index)
    {
      return _contentPresenter;
    }

    protected override int VisualChildrenCount => 1;

    /// <summary>
    /// Updates the location of the child element.
    /// </summary>
    public void UpdatePosition(double left, double top)
    {
      _leftOffset = left;
      _topOffset = top;
      if (_adornerLayer != null)
      {
        _adornerLayer.Update(AdornedElement);
      }
    }

    public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
    {
      var result = new GeneralTransformGroup();
      result.Children.Add(base.GetDesiredTransform(transform));
      result.Children.Add(new TranslateTransform(_leftOffset, _topOffset));
      return result;
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
