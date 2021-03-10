using System;
using System.Windows;
using System.Windows.Media;

namespace ES.Tools.Shapes
{
  public class PieSegment : SegmentShape
  {
    #region Properties

    protected override Geometry DefiningGeometry
    {
      get
      {
        double startAngle = 90 - StartAngle;
        double endAngle = 90 - EndAngle;

        double maxWidth = Math.Max(0.0, RenderSize.Width - StrokeThickness);
        double maxHeight = Math.Max(0.0, RenderSize.Height - StrokeThickness);

        Point start = GetPoint(startAngle, maxWidth, maxHeight);
        Point end = GetPoint(endAngle, maxWidth, maxHeight);

        StreamGeometry geometry = new StreamGeometry();
        using (StreamGeometryContext geometryContext = geometry.Open())
        {
          geometryContext.BeginFigure(new Point((RenderSize.Width / 2.0) + start.X, (RenderSize.Height / 2.0) - start.Y), true, true);
          geometryContext.ArcTo(new Point((RenderSize.Width / 2.0) + end.X, (RenderSize.Height / 2.0) - end.Y), new Size(maxWidth / 2.0, maxHeight / 2), 0.0, (EndAngle - StartAngle) > 180, SweepDirection.Clockwise, true, false);
          geometryContext.LineTo(new Point((RenderSize.Width / 2.0), (RenderSize.Height / 2.0)), true, false);
        }

        return geometry;
      }
    }

    #endregion

    #region Private Methods

    private Point GetPoint(double angle, double maxWidth, double maxHeight)
    {
      double xValue = maxWidth / 2.0 * Math.Cos(angle * Math.PI / 180.0);
      double yValue = maxHeight / 2.0 * Math.Sin(angle * Math.PI / 180.0);
      return new Point(xValue, yValue);
    }

    #endregion
  }
}
