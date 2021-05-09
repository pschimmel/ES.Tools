using System;
using System.Windows;
using System.Windows.Media;

namespace ES.Tools.Shapes
{
  /// <summary>
  /// A circle with a hole.
  /// </summary>
  public class Donut : SegmentShape
  {
    #region Donut Width Property

    public static readonly DependencyProperty DonutWidthProperty = DependencyProperty.Register(nameof(DonutWidth), typeof(double), typeof(Donut), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender, null, CoerceDonutWidth));

    public double DonutWidth
    {
      get => (double)GetValue(DonutWidthProperty);
      set => SetValue(DonutWidthProperty, value);
    }

    private static object CoerceDonutWidth(DependencyObject depObj, object value)
    {
      double size = (double)value;
      size = Math.Max(size, 0.0);
      return size;
    }

    #endregion

    #region Properties

    protected override Geometry DefiningGeometry
    {
      get
      {
        double outerWidth = Math.Max(0.0, RenderSize.Width - StrokeThickness);
        double outerHeight = Math.Max(0.0, RenderSize.Height - StrokeThickness);

        var outerCircle = new EllipseGeometry(new Rect(StrokeThickness/2, StrokeThickness/2, outerWidth, outerHeight));

        double innerWidth = Math.Max(0.0, RenderSize.Width - DonutWidth * 2 - StrokeThickness);
        double innerHeight = Math.Max(0.0, RenderSize.Height - DonutWidth * 2 - StrokeThickness);

        var innerCircle = new EllipseGeometry(new Rect(DonutWidth + StrokeThickness/2, DonutWidth + StrokeThickness/2, innerWidth, innerHeight));

        return new CombinedGeometry(GeometryCombineMode.Xor, outerCircle, innerCircle);
      }
    }

    #endregion
  }
}
