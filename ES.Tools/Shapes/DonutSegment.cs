using System;
using System.Windows;
using System.Windows.Media;

namespace ES.Tools.Shapes
{
  public class DonutSegment : SegmentShape
  {
    #region Donut Width Property

    public static readonly DependencyProperty DonutWidthProperty = DependencyProperty.Register(nameof(DonutWidth), typeof(double), typeof(DonutSegment), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender, null, CoerceDonutWidth));

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
        double startAngle = 90 - StartAngle;
        double endAngle = 90 - EndAngle;

        double maxWidth = Math.Max(0.0, RenderSize.Width - StrokeThickness);
        double maxHeight = Math.Max(0.0, RenderSize.Height - StrokeThickness);

        var start = GetOuterPoint(startAngle, maxWidth, maxHeight);
        var end = GetOuterPoint(endAngle, maxWidth, maxHeight);

        double maxWidth2 = Math.Max(0.0, RenderSize.Width - DonutWidth * 2 - StrokeThickness);
        double maxHeight2 = Math.Max(0.0, RenderSize.Height - DonutWidth * 2 - StrokeThickness);

        var start2 = GetInnerPoint(startAngle, maxWidth, maxHeight);
        var end2 = GetInnerPoint(endAngle, maxWidth, maxHeight);

        var geometry = new StreamGeometry();
        using (var geometryContext = geometry.Open())
        {
          geometryContext.BeginFigure(
            new Point(RenderSize.Width / 2.0 + start.X, RenderSize.Height / 2.0 - start.Y), true, true);
          geometryContext.ArcTo(
            new Point(RenderSize.Width / 2.0 + end.X, RenderSize.Height / 2.0 - end.Y),
            new Size(maxWidth / 2.0, maxHeight / 2.0), 0.0, EndAngle - StartAngle > 180, SweepDirection.Clockwise, true, false);
          geometryContext.LineTo(
            new Point(RenderSize.Width / 2.0 + end2.X, RenderSize.Height / 2.0 - end2.Y), true, false);
          geometryContext.ArcTo(
            new Point(RenderSize.Width / 2.0 + start2.X, RenderSize.Height / 2.0 - start2.Y),
            new Size(maxWidth2 / 2.0, maxHeight2 / 2.0), 0.0, EndAngle - StartAngle > 180, SweepDirection.Counterclockwise, true, false);
        }

        return geometry;
      }
    }

    #endregion

    #region Private Members

    private static Point GetOuterPoint(double angle, double maxWidth, double maxHeight)
    {
      double xValue = maxWidth / 2.0 * Math.Cos(angle * Math.PI / 180.0);
      double yValue = maxHeight / 2.0 * Math.Sin(angle * Math.PI / 180.0);
      return new Point(xValue, yValue);
    }

    private Point GetInnerPoint(double angle, double maxWidth, double maxHeight)
    {
      double xValue = (maxWidth / 2.0 - DonutWidth) * Math.Cos(angle * Math.PI / 180.0);
      double yValue = (maxHeight / 2.0 - DonutWidth) * Math.Sin(angle * Math.PI / 180.0);
      return new Point(xValue, yValue);
    }

    #endregion
  }
}
