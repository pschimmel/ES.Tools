using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ES.Tools.Shapes
{
  public class ThinIndicator : Shape
  {
    protected override Geometry DefiningGeometry
    {
      get
      {
        var geometry1 = new StreamGeometry();
        using (var geometryContext = geometry1.Open())
        {
          geometryContext.BeginFigure(new Point(RenderSize.Width / 2.0, 0), true, true);
          geometryContext.LineTo(new Point(RenderSize.Width / 4, RenderSize.Height - RenderSize.Width / 4), true, true);
          geometryContext.ArcTo(new Point(RenderSize.Width * 3 / 4, RenderSize.Height - RenderSize.Width / 4), new Size(RenderSize.Width / 4.0, RenderSize.Width / 4.0), 0.0, false, SweepDirection.Counterclockwise, true, false);
        }

        double radius = RenderSize.Width / 2.0 - StrokeThickness;
        var geometry2 = new EllipseGeometry(new Point(RenderSize.Width/2, RenderSize.Height*3/4), radius, radius);

        return new CombinedGeometry(GeometryCombineMode.Union, geometry1, geometry2);
      }
    }
  }
}
